using Host;

var builder = WebApplication.CreateBuilder(args);


var app = builder
    .ConfigureApplicationBuilder()
    .Build();

app
    .ConfigureApplication()
    .Run();
public partial class Program { }