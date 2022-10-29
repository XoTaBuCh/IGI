using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;

namespace WEB_053501_Sauchuk.Controllers;

public class LoadAvatarController : Controller
{
    private UserManager<ApplicationUser>? _userManager;
    private IWebHostEnvironment? _webHostEnvironment;
    private ApplicationDbContext _context;
    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    public LoadAvatarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> GetImage([FromServices] Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
    {
        var user = await GetCurrentUserAsync();

        if (user != null && user.Image?.Length > 0)
        {
            return File(user.Image, user.ContentType);
        }
        else
        {
            var avatarPath = "/images/avatar.jpeg";
            Console.WriteLine(avatarPath);
            return File(env.WebRootFileProvider
                .GetFileInfo(avatarPath)
                .CreateReadStream(), "images/...");
        }

        // var provider = env.WebRootFileProvider;
        // var path = Path.Combine("images", "avatar.jpeg");
        // var fInfo = provider.GetFileInfo(path);
        // var ext = Path.GetExtension(fInfo.Name);
        // var extProvider = new FileExtensionContentTypeProvider();
        //
        // return File(fInfo.CreateReadStream(), extProvider.Mappings[ext]);
    }

    public IActionResult Index()
    {
        return View(_context.Users.ToList());
    }
}