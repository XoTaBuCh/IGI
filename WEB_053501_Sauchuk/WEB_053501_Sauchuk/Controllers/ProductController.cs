using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;
using WEB_053501_Sauchuk.Extensions;
using WEB_053501_Sauchuk.Models;

namespace WEB_053501_Sauchuk.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    ApplicationDbContext dbContext;
    const int itemsPerPage = 3;

    public ProductController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IActionResult GetPager(int pageNo, int pages, string category)
    {
        PagerData pagerData = new PagerData() { Current = pageNo, Pages = pages, Category = category };
        return PartialView("_PagerPartial", pagerData);
    }

    [Route("Product")]
    [Route("Product/Page_{pageNo}")]
    public IActionResult Index(int pageNo = 1, string category = "Все")
    {
        List<Food> foods;
        if (category == "Все")
        {
            foods = dbContext.Foods.Include(f => f.Category).ToList();
        }
        else
        {
            Category cat = dbContext.Categories.FirstOrDefault(c => c.Name == category);
            foods = dbContext.Foods.Where(f => f.CategoryId == cat.Id).Include(f => f.Category).ToList();
        }

        foods.Sort((f1, f2) => f1.Category.Name.CompareTo(f2.Category.Name));

        ListModelView<Food> page = ListModelView<Food>.CreatePage(foods, itemsPerPage, pageNo);


        ViewData["Category"] = category;
        ViewData["Categories"] = GetCategories();

        if (Request.IsAjaxRequest())
        {
            return PartialView("_ListPartial", page);
        }
        else
        {
            return View(page);
        }
    }

    public IActionResult GetImage(int imageId)
    {
        string base64Image = dbContext.Images.FirstOrDefault(i => i.Id == imageId).Base64Image;
        return File(Some.ImageConverter.Base64ToImage(base64Image), "image/jpg");
    }

    public List<string> GetCategories()
    {
        List<string> categories = new List<string>();
        foreach (Category category in dbContext.Categories)
        {
            categories.Add(category.Name);
        }

        return categories;
    }
}