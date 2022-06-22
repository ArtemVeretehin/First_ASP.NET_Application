var builder = WebApplication.CreateBuilder();
var app = builder.Build();



app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    //var stringBuilder2 = new System.Text.StringBuilder("");
    //stringBuilder2.Append(context.Request.Headers["accept"]);

    var path = context.Request.Path;

    if (path == "/Data")
    {
        await context.Response.WriteAsync($"Дата и время: {DateTime.Now}");
    }
    else if (path == "/HostName")
    {
        await context.Response.WriteAsync($"{context.Request.HttpContext.Connection.RemoteIpAddress} {System.Convert.ToString(context.Request.HttpContext.Connection.RemotePort)} {app.Environment.ApplicationName} {context.User.Identity}");
    }       

    


    

    var stringBuilder = new System.Text.StringBuilder("<table>");

    foreach (var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
});

app.Run();