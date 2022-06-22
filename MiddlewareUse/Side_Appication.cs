namespace MIddleware_Use
{
    public class Side_Appication
    {
        public void Application_Run(WebApplication app)
        {

            app.Use(Hello_World);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<p>Good bye, World...</p>");
            });

            app.Run();
        }

        private async Task Hello_World(HttpContext context, RequestDelegate next)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<p>Hello world! <br/>");
            await next.Invoke(context);
        }
    }
}
