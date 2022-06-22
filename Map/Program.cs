var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Как я понял в Map обязательно должен присутствовать терминальный Middleware, иначе все накрывается пиздой

app.Map("/Zenit",
    appBuilder =>
    {
        appBuilder.Use(async (context, next) =>
        {
            Console.WriteLine("ZENIT VAMOS!!!");
            await next();
        });

        appBuilder.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("!!!!");
            await next();
        });
        appBuilder.Run(context => context.Response.WriteAsync("!!!"));
    });
app.Map("/Spartak", SpartakRequestHandler);

app.Run(async (context) => await context.Response.WriteAsync("ZENIT LOSE!"));


void SpartakRequestHandler(IApplicationBuilder applicationBuilder)
{
    applicationBuilder.Map("/Trophies", trophies =>
    {
        trophies.Use(async (context, next) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<p>Достижения спартака<p/><br/><p>Победитель Кубка России сезона 21/22!<p/>");
            next();
        });
        trophies.Run(async context =>
        {
            await context.Response.WriteAsync("Congrats!");
        });
    });

    applicationBuilder.Map("/RPL_Stats", stats =>
    {
        stats.Use(async (context, next) =>
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync("<p>Место в сезоне - 11<p/>");
            await next();
        });
        stats.Run(async context =>
        {
            await context.Response.WriteAsync("Беда!");
        });
    });
    applicationBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("ЗДЕСЬ МОЖНО УЗНАТЬ ОСНОВНУЮ ИНФОРМАЦИЮ О ФК Спартак!");
    });
}


app.Run();
