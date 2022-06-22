var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    if (context.Request.Path == "/old")
    {
        context.Response.Redirect("/new");
    }
    else if (context.Request.Path == "/new")
    {
        await context.Response.WriteAsync("New_Paige");
    }
    else if (context.Request.Path == "/GoGoogle")
    {
        context.Response.Redirect("Https://google.com");
    }
    else
    {
        await context.Response.WriteAsync("Main Page");
    }
});

app.Run();
