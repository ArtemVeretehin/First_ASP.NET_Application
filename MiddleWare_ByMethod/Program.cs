var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.Run(WriteHello);

app.Run();


async Task WriteHello(HttpContext context)
{
    await context.Response.WriteAsync("Hello Man!");
}