using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;

namespace WEB_053501_Sauchuk.Controllers;

public class LoadAvatarController : Controller
{
    private UserManager<ApplicationUser>? _userManager;
    private IWebHostEnvironment? _webHostEnvironment;
    private ApplicationDbContext _context;
    private SignInManager<ApplicationUser> _signInManager;

    private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    public LoadAvatarController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;

    }
    public async Task<IActionResult> GetImage([FromServices] IWebHostEnvironment env)
    {
        FileStreamResult defaultAvatar = File(env.WebRootFileProvider.GetFileInfo("/images/avatar.jpeg").CreateReadStream(), "image/jpeg");
        if (_signInManager.IsSignedIn(User))
        {
            ApplicationUser user = await _userManager?.FindByEmailAsync(User.Identity.Name);

            string base64Avatar = user.Image;
            if(base64Avatar == "")
            {
                return defaultAvatar;
            }
            return File(Some.ImageConverter.Base64ToImage(base64Avatar), "image/jpeg");
        }
        else
        {
            return defaultAvatar;
        }
    }
}