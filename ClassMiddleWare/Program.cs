var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();




//�������� ����������� �����, ��� �������� next
//TokenMiddleWare tokenMiddleWare = new TokenMiddleWare(FinalAction);
//��������� �������� ����� ���������� ������
//app.Run(tokenMiddleWare.InvokeAsync);
//�� ��� ������ ������ � ����� ����� �� �����, �� ������ ��� ������


//������ ����� ������������� �������� � ����������� TokenMiddleWare �� ����� ��������� next ������ middleware. � ������ ����� ������������ ����� InvokeAsyncs
app.UseMiddleware<TokenMiddleWare>("1234",app);

//�� �� �����, �� ����� ����� ����������
app.UseToken("veretehin");

//������� MiddleWare-������
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