

using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var response = context.Response;

    //���� ���� ��������� � ������ CreateUser, �� �������� ������������ ��� html-�������� � ������� ����� ����� ������ ������������
    //��� ������ ������ ����� ��������� � ������������ ������ ������ ������������� ��������� ������������ ������ � /api/user � ���� ���������� json ������
    //����� ����� ��������� ������ ����� else if
    if (request.Path == "/CreateUser")
    {
        response.Headers.ContentType = "text/html";
        await response.SendFileAsync("html/index.html");
    }
    else if (request.Path == "/api/user")
    {
        string message = "������������ ������";
 
        //���������� ������ � JSON � ���������� ������������
        if (request.HasJsonContentType())
        {
            //���������� ��������� ������������/��������������
            var jsonoptions = new JsonSerializerOptions();
            //��������� ��������� ���� json � ������ ���� Person
            jsonoptions.Converters.Add(new PersonConverter());
            //������������� ������ � ������� ���������� PersonConverter
            var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
            //���� ����������, �� ���������� ����� ���������
            if (person is not null)
            { 
                message = $"Name = {person.Name}, Age = {person.Age}";
            }
        }

        //���������� ������� ����� � ���� JSON-������ (����������� message)
        await response.WriteAsJsonAsync(new {text = message});
    }
    Person tom = new("Tom", 22);
   
    //await context.Response.WriteAsJsonAsync(tom);
});

app.Run();

public record Person(string Name, int Age);
public class PersonConverter : JsonConverter<Person>
{
    //�����, ����������� �������������� �� Json � Person
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
                    // ���� �������� Age/age � ��� �������� �����
                    case "age" or "Age" when reader.TokenType == JsonTokenType.Number:
                        personAge = reader.GetInt32();  // ��������� ����� �� json
                        break;
                    // ���� �������� Age/age � ��� �������� ������
                    case "age" or "Age" when reader.TokenType == JsonTokenType.String:
                        string? stringValue = reader.GetString();
                        // �������� �������������� ������ � �����
                        if (int.TryParse(stringValue, out int value))
                        {
                            personAge = value;
                        }
                        break;
                    case "Name" or "name":  // ���� �������� Name/name
                        string? name = reader.GetString();
                        if (name != null)
                            personName = name;
                        break;
                }
            }
        }
        return new Person(personName, personAge);
    }
    // //�����, ����������� ������������ �� Person � Json
    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);

        writer.WriteEndObject();
    }
}

