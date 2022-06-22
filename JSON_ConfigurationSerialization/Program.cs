

using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var response = context.Response;

    //≈сли было обращение к адресу CreateUser, то браузеру возвращаетс€ код html-страницы в которой можно вбить данные пользовател€
    // ак только данные будут заполнены и пользователь нажмет кнопку подтверждени€ автоматом произведетс€ запрос к /api/user и туда запоститс€ json строка
    //ѕосле этого обработка пойдет через else if
    if (request.Path == "/CreateUser")
    {
        response.Headers.ContentType = "text/html";
        await response.SendFileAsync("html/index.html");
    }
    else if (request.Path == "/api/user")
    {
        string message = "Ќекорректные данные";
 
        //ѕроизводим работу с JSON с настройкой сериализации
        if (request.HasJsonContentType())
        {
            //определ€ем параметры сериализации/десериализации
            var jsonoptions = new JsonSerializerOptions();
            //добавл€ем конверетр кода json в объект типа Person
            jsonoptions.Converters.Add(new PersonConverter());
            //десериализуем данные с помощью конвертера PersonConverter
            var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
            //≈сли получилось, то составл€ем новое сообщение
            if (person is not null)
            { 
                message = $"Name = {person.Name}, Age = {person.Age}";
            }
        }

        //¬озвращаем клиенту ответ в виде JSON-строки (сериализуем message)
        await response.WriteAsJsonAsync(new {text = message});
    }
    Person tom = new("Tom", 22);
   
    //await context.Response.WriteAsJsonAsync(tom);
});

app.Run();

public record Person(string Name, int Age);
public class PersonConverter : JsonConverter<Person>
{
    //ћетод, выполн€ющий десериализацию из Json в Person
    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var personName = "Undefined";
        var personAge = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                switch (propertyName)
                {
                    // если свойство Age/age и оно содержит число
                    case "age" or "Age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // считываем число из json
                        break;
                    // если свойство Age/age и оно содержит строку
                    case "age" or "Age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // пытаемс€ конвертировать строку в число
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "Name" or "name":  // если свойство Name/name
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personName, personAge);
    }
    // //ћетод, выполн€ющий сериализацию из Person в Json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);

        writer.WriteEndObject();
    }
}

