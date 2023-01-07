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
        private readonly AppIdentityDbContext context;
        public HomeController(UserManager<AppUser> usrMng, AppIdentityDbContext context)
        {
            userManager = usrMng;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            //return View(context.Products);
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
