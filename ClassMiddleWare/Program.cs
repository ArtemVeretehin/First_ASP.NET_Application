var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




//Передали собственный метод, как параметр next
//TokenMiddleWare tokenMiddleWare = new TokenMiddleWare(FinalAction);
//Запускаем корневой метод созданного класса
//app.Run(tokenMiddleWare.InvokeAsync);
//То что сделал сверху я имеет право на жизнь, но видимо это колхоз


//Данный метод автоматически внедряет в конструктор TokenMiddleWare на место параметра next объект middleware. И видимо сразу определяется вызов InvokeAsyncs
app.UseMiddleware<TokenMiddleWare>("1234",app);

//То же самое, но через метод расширения
app.UseToken("veretehin");

//Создаем MiddleWare-объект
app.Run(FinalAction);

app.Run();


async Task FinalAction(HttpContext context)
{
    context.Response.ContentType = "text/html";
    if (context.Request.Query["name"] == "artem")
    {
        await context.Response.WriteAsync("Artem<br/>");        
    }
    await context.Response.WriteAsync("Token is valid!");
}