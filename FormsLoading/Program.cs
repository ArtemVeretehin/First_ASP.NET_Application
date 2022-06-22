var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // если обращение идет по адресу "/postuser", получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        IFormCollection form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        await context.Response.WriteAsync($"<div><p>Name: {name}</p><p>Age: {age}</p></div>");
    }
    else if (context.Request.Path == "/postuser_Array")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        string[] languages = form["languages"];
        string langs = "";
        string li_langs = "";
        foreach (string lang in languages)
        {
            li_langs = string.Concat(li_langs, "<li>", lang, "</li>");
        }
        langs = string.Concat("<ul>", li_langs, "</ul>");

        string[] prog_languages = form["prog_languages"];
        string prog_langs = "";
        string li_prog_langs = "";
        foreach (string prog_lang in prog_languages)
        {
            li_prog_langs = string.Concat(li_prog_langs, "<li>", prog_lang, "</li>");
        }
        prog_langs = string.Concat("<ul>", li_prog_langs, "</ul>"); 
        await context.Response.WriteAsync($"<p>Name: {name}</p> <p>Age: {age}</p>Языки у пользователя {name}:{langs} </br> Любимые языки программирования у пользователя {name}:{prog_langs}");
    }
    else if (context.Request.Path == "/Form_Array")
    {
        await context.Response.SendFileAsync("html/index_arrays.html");
    }
    else if (context.Request.Path == "/Form")
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});

app.Run();