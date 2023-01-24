using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using CoffeeBean.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private readonly AppIdentityDbContext dbcontext;

        public AccountController(UserManager<AppUser> usrMng, SignInManager<AppUser> sgnInMng, AppIdentityDbContext context)
        {
            userManager = usrMng;
            signInManager = sgnInMng;
            dbcontext = context;
        }

        [AllowAnonymous]
        public ViewResult SignUp() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);

                if(appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, login.RememberMe, false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> AddToCart(string productId)
        //{
        //    Product prod = await dbcontext.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
        //    var appuser = HttpContext.User.Identity;
        //}
    }
}
