var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var request = context.Request;
    var response = context.Response;
    response.ContentType = "text/html; charset=utf-8";
    if (request.Path == "/upload")
    { 
        var form = request.Form;
        //string[] Files_What = form["uploads"];
        
        IFormFileCollection files = request.Form.Files;
        var uploadPath = Directory.GetCurrentDirectory() + "/Uploads";
        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            // ���� � ����� uploads
            string fullPath = $"{uploadPath}/{file.FileName}";

            // ��������� ���� � ����� uploads
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("����� ������� ���������");
    }
    else
    {  
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();