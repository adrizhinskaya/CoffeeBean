using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using CoffeeBean.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private readonly AppIdentityDbContext dbcontext;
        private readonly IWebHostEnvironment appEnvironment;

        public AccountController(UserManager<AppUser> usrMng, SignInManager<AppUser> sgnInMng, AppIdentityDbContext context, IWebHostEnvironment appEnv)
        {
            userManager = usrMng;
            signInManager = sgnInMng;
            dbcontext = context;
            appEnvironment = appEnv;
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
                    await userManager.AddToRoleAsync(appUser, "User");
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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Cart(string id)
        {
            Product prod = await dbcontext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            user.Cart.Add(prod);
            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Index", "ProductsReview");
            }
            else
            {
                Errors(result);
                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddToCart(string id)
        {
            Product prod = await dbcontext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            user.Cart.Add(prod);
            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("Product", "ProductsReview", new { id });
            }
            else
            {
                Errors(result);
                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string id)
        {
            Product prod = await dbcontext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            user.Cart.Remove(prod);
            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await dbcontext.SaveChangesAsync();
                return RedirectToAction("CartReview");
            }
            else
            {
                Errors(result);
                return BadRequest();
            }
        }

        [Authorize(Roles = "User")]
        public IActionResult CartReview()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = dbcontext.Products.Where(p => p.AppUserId == userId);
            return View(cart);
        }

        [Authorize(Roles = "User")]
        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = dbcontext.Products.Where(p => p.AppUserId == userId);

            string file_path = Path.Combine(appEnvironment.ContentRootPath, "wwwroot/files/order.txt");
            string file_type = "application/txt";
            string file_name = "order.txt";

            var file = new FileInfo(file_path);
            FileStream stream = file.Open(FileMode.OpenOrCreate);
            stream.Close();

            StreamWriter streamWriter = file.CreateText();
            streamWriter.Flush();
            streamWriter.WriteLine($"The number of products: {cart.Count()}");
            decimal totalAmount = 0;
            foreach (var prod in cart)
            {
                streamWriter.WriteLine($"{prod.Name} - {prod.Price}$");
                totalAmount += prod.Price;
            }
            streamWriter.WriteLine($"\nTotal amount: {totalAmount}$");
            streamWriter.Close();

            return PhysicalFile(file_path, file_type, file_name);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
    }
}
