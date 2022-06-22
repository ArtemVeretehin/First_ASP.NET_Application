
public class TokenMiddleWare
{
    private readonly RequestDelegate next;
    WebApplication app;
    private string pattern;

    public TokenMiddleWare(RequestDelegate next,string pattern, WebApplication app)
    {
        this.next = next;
        this.pattern = pattern;
        this.app = app;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        app.Environment.EnvironmentName = "Production";
        if (app.Environment.IsEnvironment("Development")) Console.WriteLine(app.Environment.EnvironmentName);
        if (app.Environment.IsEnvironment("Production")) Console.WriteLine(app.Environment.EnvironmentName);
        var token = context.Request.Query["token"];
        if (token != pattern)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}

public class TokenMiddleWare2
{
    private readonly RequestDelegate next;
    private string pattern;
    public TokenMiddleWare2(RequestDelegate next, string pattern)
    {
        this.next = next;
        this.pattern = pattern;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Query["surname"]!=pattern)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Surname isn't valid!");
        }  
        else
        {
            await next.Invoke(context);
        }
    }
}


//Метод расширения

public static class TokenExtensions
{
    public static IApplicationBuilder UseToken(this IApplicationBuilder appBuilder, string pattern)
    {
        return appBuilder.UseMiddleware<TokenMiddleWare2>(pattern);
    }
}

