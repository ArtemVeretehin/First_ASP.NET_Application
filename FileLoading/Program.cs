using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (HttpContext context) =>
{
    if (context.Request.Path == "/Artem")
    {
        await context.Response.SendFileAsync("C:\\Users\\Артем\\Desktop\\TOR-ARTEM.jpg");
    }
    else if (context.Request.Path == "/ARTEM_PROJECT")
    {
        context.Response.Headers.ContentDisposition= "attachment; filename = Krutoy Artem.jpg";
        await context.Response.SendFileAsync("TOR-ARTEM.jpg");
    }
    else if (context.Request.Path == "/MyHTML")
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync("html/index.html");
    }
    else if (context.Request.Path == "/ARTEM_ByFileInfo")
    {
        PhysicalFileProvider fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        IFileInfo file = fileProvider.GetFileInfo("TOR-ARTEM.jpg");
        context.Response.Headers.ContentDisposition ="attachment";
        await context.Response.SendFileAsync(file);
    }
    else
    {
        await context.Response.WriteAsync("HELLO ARTEM");
    }
});

app.Run();
