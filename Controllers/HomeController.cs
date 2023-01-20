using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> userManager;
        private readonly AppIdentityDbContext dbcontext;
        public HomeController(UserManager<AppUser> usrMng, AppIdentityDbContext context)
        {
            userManager = usrMng;
            dbcontext = context;
        }

        public IActionResult Index()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories = dbcontext.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.Category = categories;

            return View(dbcontext.Products);
        }
    }
}
