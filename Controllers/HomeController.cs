using CoffeeBean.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> usrMng)
        {
            userManager = usrMng;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            AppUser appUser = await userManager.GetUserAsync(HttpContext.User);
            string mes = $"Hello {appUser.UserName}";
            return View((object)mes);
        }
    }
}
