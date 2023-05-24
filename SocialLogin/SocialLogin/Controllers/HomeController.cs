using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialLogin.Models;
using System.Diagnostics;

namespace SocialLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //await _userManager.CreateAsync(new AppUser { FullName = "Name1 Surname1", Email = "name1@gmail.com",UserName="name1"},"Name@1");
            //await _userManager.CreateAsync(new AppUser { FullName = "Name2 Surname2", Email = "name2@gmail.com", UserName = "name2" }, "Name@2");
            //await _userManager.CreateAsync(new AppUser { FullName = "Name3 Surname3", Email = "name3@gmail.com", UserName = "name3" }, "Name@3");

            return View();
        }

    }
}