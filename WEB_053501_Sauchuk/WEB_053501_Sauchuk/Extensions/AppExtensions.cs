using WEB_053501_Sauchuk.Middleware;

namespace WEB_053501_Sauchuk.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogMiddleware>();
        }
    }
}