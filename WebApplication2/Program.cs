var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int x = 2;
app.Run(async (context) =>
{
    x = x * 2;
    await context.Response.WriteAsync($"Result: {x}");
    //context.Response.Redirect("localhost:7232");
});
app.Run();
