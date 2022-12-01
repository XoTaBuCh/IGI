using Microsoft.AspNetCore.Mvc;
using WEB_053501_Sauchuk.Models;
using WEB_053501_Sauchuk.Extensions;

namespace WEB_053501_Sauchuk.Components;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        Cart cart = HttpContext.Session.Get<Cart>("cart");
        return View(cart);
    }
}