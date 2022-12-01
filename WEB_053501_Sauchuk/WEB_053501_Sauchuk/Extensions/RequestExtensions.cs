namespace WEB_053501_Sauchuk.Extensions;

public static class RequestExtensions
{
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        return request.Headers["x-requested-with"] == "XMLHttpRequest";
    }
}