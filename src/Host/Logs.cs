namespace Host;

internal static partial class Logs
{
    [LoggerMessage(
        Message = @"Incoming http {httpMethod} {uri} failed ({httpCode}); 
        headers: {headers}; 
        queryParameters: {queryParameters};
        request: {jsonRequestPayload};
        response: {responseJsonBody}")]
    public static partial void IncomingRequestFailed(
        this ILogger logger,
        LogLevel logLevel,
        int httpCode,
        string httpMethod,
        string uri,
        string headers,
        string queryParameters,
        string jsonRequestPayload,
        string responseJsonBody);

}
