using System.Text;

namespace Host;

internal class LogFailedIncomingRequestMiddleware(
    RequestDelegate next,
    ILogger<LogFailedIncomingRequestMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<LogFailedIncomingRequestMiddleware> _logger = logger;

    private readonly string[] _sensitiveHeadersKeys = ["Authorization"];

    public async Task InvokeAsync(HttpContext context)
    {
        /// Capture request details
        var request = context.Request;

        // Read the body of the request
        var jsonRequestPayload = await ReadRequestBodyAsync(request);

        // Capture the original response body stream
        var originalResponseBodyStream = context.Response.Body;

        // Create a new memory stream to capture the response
        var responseBody = new MemoryStream();
        // Set the response body to the memory stream
        context.Response.Body = responseBody;

        // Proceed with the request pipeline
        await _next(context);

        // Read the response body from the memory stream
        responseBody.Seek(0, SeekOrigin.Begin);
        var jsonResponseBody = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);

        // If the status code indicates an error (non-2xx)
        if (context.Response.StatusCode >= 400)
        {
            // Log the details of the request and response
            var logLevel = context.Response.StatusCode >= 500 ? LogLevel.Error : LogLevel.Warning;
            var headers = string.Join(", ", request.Headers
                .Where(h => !_sensitiveHeadersKeys.Contains(h.Key))
                .Select(h => $"{h.Key}: {h.Value}"));
            var queryParameters = string.Join(", ", request.Query.Select(q => $"{q.Key}: {q.Value}"));

            _logger.IncomingRequestFailed(
                logLevel: logLevel,
                httpCode: context.Response.StatusCode,
                httpMethod: context.Request.Method,
                uri: request.Path,
                headers: headers,
                queryParameters: queryParameters,
                jsonRequestPayload: jsonRequestPayload,
                responseJsonBody: jsonResponseBody);
        }

        // Copy the response body back to the original stream
        await responseBody.CopyToAsync(originalResponseBodyStream);
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        // Reset the body stream position
        request.EnableBuffering();

        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var jsonRequestPayload = await reader.ReadToEndAsync();
        request.Body.Position = 0; // Reset the position so other middleware can read the request body
        return jsonRequestPayload;
    }
}

