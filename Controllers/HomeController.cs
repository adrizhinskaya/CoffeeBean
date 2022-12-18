using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            AppUser appUser = await userManager.GetUserAsync(HttpContext.User);
            if (appUser != null)
            {
                string mes = $"Hello {appUser.UserName}";
                return View((object)mes);
            }

            return View();
        }
    }
}
