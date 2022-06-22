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
        try
        {
        //���� ��������� �� ������� JSON �������� � �������
        //if (request.HasJsonContentType())
            //�������� ����������������� ������ �� JSON-������ � ����� ���� Person
            var person = await request.ReadFromJsonAsync<Person>();
            //���� ����������, �� ���������� ����� ���������
            if (person is not null)
            { 
                message = $"Name = {person.Name}, Age = {person.Age}";
            }
        }
        catch { }
        //���������� ������� ����� � ���� JSON-������ (����������� message)
        await response.WriteAsJsonAsync(new {text = message});
    }
    Person tom = new("Tom", 22);
   
    //await context.Response.WriteAsJsonAsync(tom);
});

app.Run();

public record Person(string Name, int Age);

