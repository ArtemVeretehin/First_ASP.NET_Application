var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var response = context.Response;

    //Если было обращение к адресу CreateUser, то браузеру возвращается код html-страницы в которой можно вбить данные пользователя
    //Как только данные будут заполнены и пользователь нажмет кнопку подтверждения автоматом произведется запрос к /api/user и туда запостится json строка
    //После этого обработка пойдет через else if
    if (request.Path == "/CreateUser")
    {
        response.Headers.ContentType = "text/html";
        await response.SendFileAsync("html/index.html");
    }
    else if (request.Path == "/api/user")
    {
        string message = "Некорректные данные";
        try
        {
        //ЛИБО ПРОВЕРЯЕМ НА НАЛИЧИЕ JSON контента в запросе
        //if (request.HasJsonContentType())
            //Пытаемся десереализировать данные из JSON-строки в объет типа Person
            var person = await request.ReadFromJsonAsync<Person>();
            //Если получилось, то составляем новое сообщение
            if (person is not null)
            { 
                message = $"Name = {person.Name}, Age = {person.Age}";
            }
        }
        catch { }
        //Возвращаем клиенту ответ в виде JSON-строки (сериализуем message)
        await response.WriteAsJsonAsync(new {text = message});
    }
    Person tom = new("Tom", 22);
   
    //await context.Response.WriteAsJsonAsync(tom);
});

app.Run();

public record Person(string Name, int Age);

