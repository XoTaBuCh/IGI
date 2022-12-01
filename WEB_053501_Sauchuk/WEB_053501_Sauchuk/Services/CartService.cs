using Newtonsoft.Json;
using WEB_053501_Sauchuk.Entities;
using WEB_053501_Sauchuk.Extensions;
using WEB_053501_Sauchuk.Models;

namespace WEB_053501_Sauchuk.Services;

public class CartService : Cart
{
    [JsonIgnore] ISession Session { get; set; }

    public static Cart GetCart(IServiceProvider serviceProvider)
    {
        ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()
            .HttpContext
            .Session;

        CartService cart = session.Get<CartService>("cart");

        cart.Session = session;
        return cart;
    }

    public override void AddToCart(Food item)
    {
        base.AddToCart(item);
        Session.Set("cart", this);
    }

    public override void RemoveFormCart(Food item)
    {
        base.RemoveFormCart(item);
        Session.Set("cart", this);
    }

    public override void RemoveFormCart(int id)
    {
        base.RemoveFormCart(id);
        Session.Set("cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session.Set("cart", this);
    }
}