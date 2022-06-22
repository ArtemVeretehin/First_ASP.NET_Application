using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

//Создаем ветку в которой как бы есть объект берущий на себя роль app - appBuilder в который встраиваем дополнительные MiddleWare
//При этом если в данной ветке выполнены все middleWare и среди них не было терминального, то производится возврат в основную ветку
//ВАЖНО: Ветка создается только один раз, ВСЕ параметры, значения которых определены за пределами MiddleWare будут инициализированы однократно при запуске приложения

app.UseWhen(
    (HttpContext context) => context.Request.Path == "/time", // если путь запроса "/time"
    TimeWorkHandler);


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello ARTEM!");
    await next();
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello METANIT.COM");
});


async void TimeWorkHandler(IApplicationBuilder appBuilder)
{
    appBuilder.Use(async (context, next) =>
    {
        var time = DateTime.Now.ToShortTimeString();
        Console.WriteLine($"Time: {time}");
        await next();   // вызываем следующий middleware
    });
    appBuilder.Use(async (context, next) =>
    {
        var time = DateTime.Now.ToShortTimeString();
        await next();
        await context.Response.WriteAsync($"Time: {time}");
    });
}


app.Run();