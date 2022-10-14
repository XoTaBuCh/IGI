using Microsoft.AspNetCore.Mvc;

namespace WEB_053501_Sauchuk.Components;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}