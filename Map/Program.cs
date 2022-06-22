var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//��� � ����� � Map ����������� ������ �������������� ������������ Middleware, ����� ��� ����������� ������

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
            await context.Response.WriteAsync("<p>���������� ��������<p/><br/><p>���������� ����� ������ ������ 21/22!<p/>");
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
            await context.Response.WriteAsync("<p>����� � ������ - 11<p/>");
            await next();
        });
        stats.Run(async context =>
        {
            await context.Response.WriteAsync("����!");
        });
    });
    applicationBuilder.Run(async context =>
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync("����� ����� ������ �������� ���������� � �� �������!");
    });
}


app.Run();
