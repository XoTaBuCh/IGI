using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_053501_Sauchuk.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> _listDemo;

        public HomeController()
        {
            _listDemo = new List<ListDemo>
            {
                new() { ListItemValue=1, ListItemText="Item 1"},
                new() { ListItemValue=2, ListItemText="Item 2"},
                new() { ListItemValue=3, ListItemText="Item 3"}
            };
        }

        public IActionResult Index()
        {
            ViewData["Text"] = "Лабораторная работа №3";
            ViewData["Lst"] = new SelectList(_listDemo,"ListItemValue","ListItemText");
            return View();
        }
    }

    public class ListDemo
    {
        public int ListItemValue { get; set; }
        public string ListItemText { get; set; }
    }
}