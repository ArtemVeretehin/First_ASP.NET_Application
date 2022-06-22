using MIddleware_Use;

var appBuilder = WebApplication.CreateBuilder();
var app = appBuilder.Build();
//app.UseDefaultFiles();
//app.UseStaticFiles();
Console.WriteLine("Какую часть приложения вы хотите запустить?\n" +
    "Нажмите 1, чтобы запустить Основное приложение\n" +
    "Нажмите 2, чтобы запустить Побочное приложение");
byte application_number = System.Convert.ToByte(Console.ReadLine());

switch (application_number)
{
    case 1:
        
        app.Use(async (context, next) =>
        {
            string name = System.Convert.ToString(context.Request.Path).Replace("/", "");
            await context.Response.WriteAsync($"1.hello {name}\n");
            await next.Invoke();
            await context.Response.WriteAsync($"4.hello {name} valerich veretehin from tomsk!\n");
        });

        /*
        app.Use(async (context, next) =>
        {
            string name = System.Convert.ToString(context.Request.Path).Replace("/", "");
            await context.Response.WriteAsync($"2.hello {name} valerich!\n");
            await next.Invoke();

        });*/

        //app.Map("/{name:alpha}", (HttpContext context,string name) => context.Response.WriteAsync($"3.hello {name} valerich veretehin\n"));        
        app.MapGet("/", (context) => context.Response.WriteAsync("3.hello artem valerich veretehin\n"));
        
        app.Run();
        break;
    case 2:
        Side_Appication side_application = new Side_Appication();
        side_application.Application_Run(app);
        break;
    default:
        Console.WriteLine("неизвестное приложение");
        break;
}





