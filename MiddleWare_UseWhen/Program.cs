using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

//������� ����� � ������� ��� �� ���� ������ ������� �� ���� ���� app - appBuilder � ������� ���������� �������������� MiddleWare
//��� ���� ���� � ������ ����� ��������� ��� middleWare � ����� ��� �� ���� �������������, �� ������������ ������� � �������� �����
//�����: ����� ��������� ������ ���� ���, ��� ���������, �������� ������� ���������� �� ��������� MiddleWare ����� ���������������� ���������� ��� ������� ����������

app.UseWhen(
    (HttpContext context) => context.Request.Path == "/time", // ���� ���� ������� "/time"
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
        await next();   // �������� ��������� middleware
    });
    appBuilder.Use(async (context, next) =>
    {
        var time = DateTime.Now.ToShortTimeString();
        await next();
        await context.Response.WriteAsync($"Time: {time}");
    });
}


app.Run();