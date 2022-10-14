using Microsoft.AspNetCore.Mvc;
using WEB_053501_Sauchuk.Models;

namespace WEB_053501_Sauchuk.Components;

public class MenuViewComponent : ViewComponent
{
    // Инициализация списка элементов меню
    List<MenuItem> _menuItems = new()
    {
        new() { Controller = "Home", Action = "Index", Text = "Lab 3" },
        new() { Controller = "Product", Action = "Index", Text = "Каталог" },
        new() { IsPage = true, Area = "Admin", Page = "/Index", Text = "Администрирование" }
    };

    public IViewComponentResult Invoke()
    {
        //Получение значений сегментов маршрута
        var controller = ViewContext.RouteData.Values["controller"];
        var page = ViewContext.RouteData.Values["page"];
        var area = ViewContext.RouteData.Values["area"];

        foreach (var item in _menuItems)
        {
            // Название контроллера совпадает?
            var _matchController = controller?.Equals(item.Controller) ?? false;

            // Название зоны совпадает?
            var _matchArea = area?.Equals(item.Area) ?? false;

            // Если есть совпадение, то сделать элемент меню активным
            // (применить соответствующий класс CSS)
            if (_matchArea || _matchController)
            {
                item.Active = "active";
            }
        }

        return View(_menuItems);
    }
}