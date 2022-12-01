using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;
using WEB_053501_Sauchuk.Models;
namespace WEB_053501_Sauchuk.Some;

public static class DbInitializer
{
    //ApplicationDbContext dbContext;

    //public UserManager<ApplicationUser> userManager;
    //private readonly RoleManager<IdentityRole> roleManager;

    // public DBInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
    //     RoleManager<IdentityRole> roleManager)
    // {
    //     this.dbContext = dbContext;
    //     this.userManager = userManager;
    //     this.roleManager = roleManager;
    // }

    public static async Task Initialize(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Users.Count() > 0)
        {
            return;
        }

        IdentityRole admin = new IdentityRole("admin");
        IdentityRole user = new IdentityRole("user");

        await roleManager.CreateAsync(admin);
        await roleManager.CreateAsync(user);

        ApplicationUser adminUser = new ApplicationUser()
        {
            Email = "admin@admin.com",
            EmailConfirmed = true,
            UserName = "admin@admin.com",
            Image = ""
        };

        await userManager.CreateAsync(adminUser, "qwerty");
        await userManager.AddToRoleAsync(adminUser, "admin");

        ApplicationUser defaultUser = new ApplicationUser()
        {
            Email = "user@user.com",
            EmailConfirmed = true,
            UserName = "user@user.com",
            Image = ""
        };

        await userManager.CreateAsync(defaultUser, "qwerty");
        await userManager.AddToRoleAsync(defaultUser, "user");


        Category garnish = new Category() { Name = "Гарнир" };
        Category salad = new Category() { Name = "Салаты" };
        Category meat = new Category() { Name = "Мясное" };
        Category drink = new Category() { Name = "Напитки" };
        Category snack = new Category() { Name = "Перекус" };
        Category dessert = new Category() { Name = "Сладкое" };

        List<Food> desserts = new List<Food>()
        {
            //dessert
            new Food
            {
                Name = "Ромовая баба",
                Category = dessert,
                Description =
                    "Разновидность бабы — кекса, изготовленного из сдобного дрожжевого теста с добавлением изюма, иногда цукатов",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\dessert\ромовая баба.jpg")
                },
                Price = 1.21f
            },
            new Food
            {
                Name = "Сочник",
                Category = dessert,
                Description = "Выпечка в виде сложенной пополам лепёшки с полуоткрытой или закрытой начинкой",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\dessert\сочник.jpg")
                },
                Price = 1.50f
            },
            new Food
            {
                Name = "Сырники",
                Category = dessert,
                Description =
                    "Блюдо белорусской, русской, украинской и молдавской кухонь в виде обжаренных лепёшек из творога и муки",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\dessert\сырники.jpg")
                },
                Price = 2.10f
            },
        };
        List<Food> drinks = new List<Food>()
        {
            //drink
            new Food
            {
                Name = "Чай Nestea",
                Category = drink,
                Description = "Уникальный по своей натуральности и пользе напиток",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\drink\чай нести.jpg")
                },
                Price = 1.80f
            },
        };
        List<Food> garnishes = new List<Food>()
        {
            //garnish
            new Food
            {
                Name = "Гречка",
                Category = garnish,
                Description =
                    "Каша, приготавливаемая из гречневой крупы, популярное блюдо русской, белорусской, украинской, литовской и польской кухонь.",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\garnish\гречка.jpg")
                },
                Price = 0.71f
            },
            new Food
            {
                Name = "Картошка фри",
                Category = garnish,
                Description = "Кусочки картофеля, обжаренные во фритюре",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\garnish\картошка фри.jpg")
                },
                Price = 1.51f
            },
            new Food
            {
                Name = "Макароны",
                Category = garnish,
                Description = "Макаронные изделия в виде трубочек разного диаметра и длины",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(
                            Environment.CurrentDirectory + @"\wwwroot\images\garnish\макароны.jpg")
                },
                Price = 0.72f
            },
        };
        //meat
        List<Food> meatDishes = new List<Food>()
        {
            new Food
            {
                Name = "Колдуны с мясом",
                Category = meat,
                Description = "Драники с мясной начинкой",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\meat\колдуны с мясом.jpg")
                },
                Price = 1.90f
            },
            new Food
            {
                Name = "Котлета \"Папараць-кветка\"",
                Category = meat,
                Description = "Вкусная куриная котлета с начинкой из сыра и сливочного масла",
                Image = new Entities.Image()
                {
                    Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                               @"\wwwroot\images\meat\котлета папараць-кветка.jpg")
                },
                Price = 1.87f
            },
            new Food
            {
                Name = "Котлета с грибами",
                Category = meat,
                Description = "Куриные котлеты с грибами",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\meat\котлета с грибами.jpg")
                },
                Price = 1.78f
            },
            new Food
            {
                Name = "Мясо по-французски",
                Category = meat,
                Description = "Блюдо, приготовленное из слоёв мяса, картофеля и сыра",
                Image = new Entities.Image()
                {
                    Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                               @"\wwwroot\images\meat\мясо по-французски.jpg")
                },
                Price = 1.98f
            },
            new Food
            {
                Name = "Сосиски",
                Category = meat,
                Description = "Какие-то сосиски",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\meat\сосиска.jpg")
                },
                Price = 0.98f
            },
            new Food
            {
                Name = "Шашлык",
                Category = meat,
                Description = "Редкий гость нашего буфета",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\meat\шашлык.jpg")
                },
                Price = 1.99f
            },
        };
        //salad
        List<Food> salads = new List<Food>()
        {
            new Food
            {
                Name = "Салат из капусты",
                Category = salad,
                Description = "Свежий лёгкий салат",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\salad\салат из капусты.jpg")
                },
                Price = 1.27f
            },
            new Food
            {
                Name = "Салат из помидор",
                Category = salad,
                Description = "Помидоры, немного перца и щепотка укропа. Либо просто помидоры с луком, если не повезло",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\salad\салат из помидор.jpg")
                },
                Price = 1.32f
            },
            new Food
            {
                Name = "Салат с крабовыми палочками",
                Category = salad,
                Description = "Салат с крабовыми палочками, которые не из краба, если что",
                Image = new Entities.Image()
                {
                    Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                               @"\wwwroot\images\salad\салат с крабовыми палочками.jpg")
                },
                Price = 1.57f
            },
        };
        List<Food> snacks = new List<Food>()
        {
            new Food
            {
                Name = "Блин с ветчиной и сыром",
                Category = snack,
                Description = "Можно добавить грибы, морковь, огурцы, майонез, кетчуп..",
                Image = new Entities.Image()
                {
                    Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                               @"\wwwroot\images\snack\блин с ветчиной и сыром.jpg")
                },
                Price = 3.40f
            },
            new Food
            {
                Name = "Бутерброд с ветчиной и помидорами",
                Category = snack,
                Description = "Просто бутерброд",
                Image = new Entities.Image()
                {
                    Base64Image = ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                               @"\wwwroot\images\snack\бутерброд с ветчиной и помидорами.jpg")
                },
                Price = 0.87f
            },
            new Food
            {
                Name = "Пицца школьная",
                Category = snack,
                Description = "Но так-то уже студенческая",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory +
                                                     @"\wwwroot\images\snack\пицца школьная.jpg")
                },
                Price = 1.87f
            },
            new Food
            {
                Name = "Хот-дог с охотничьей колбаской",
                Category = snack,
                Description = "Самый простой хот-дог",
                Image = new Entities.Image()
                {
                    Base64Image =
                        ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\wwwroot\images\snack\хот-дог.jpg")
                },
                Price = 2.13f
            },
        };

        dbContext.Foods.AddRange(desserts);
        dbContext.Foods.AddRange(drinks);
        dbContext.Foods.AddRange(garnishes);
        dbContext.Foods.AddRange(salads);
        dbContext.Foods.AddRange(snacks);
        dbContext.SaveChanges();
    }
}