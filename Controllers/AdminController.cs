using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using CoffeeBean.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher; // получаем хэшированное значение пароля пользователя
        private readonly AppIdentityDbContext dbcontext;
        public AdminController(UserManager<AppUser> usrMng, IPasswordHasher<AppUser> pwHash, AppIdentityDbContext context)
        {
            userManager = usrMng;
            passwordHasher = pwHash;
            dbcontext = context;
        }
        #region CategoriesManaging
        public IActionResult Categories()
        {
            return View(dbcontext.Categories);
        }

        public ViewResult CreateCategory() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await dbcontext.Categories.AddAsync(category);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> UpdateCategory(string id)
        {
            Category category = await dbcontext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            dbcontext.Update(category);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = new Category() { Id = id };
            dbcontext.Remove(category);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Categories");
        }
        #endregion

        #region ProductManaging
        public IActionResult Products()
        {
            return View(dbcontext.Products);
        }
        #endregion

        #region UserManaging
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Update(string id)
        {
            AppUser appUser = await userManager.FindByIdAsync(id);

            if(appUser != null)
            {
                return View(appUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser appUser = await userManager.FindByIdAsync(id);

            if(appUser != null)
            {
                if(!string.IsNullOrEmpty(email))
                {
                    appUser.Email = email;
                }
                else
                {
                    ModelState.AddModelError("", "E-mail cannot be empty");
                }

                if(!string.IsNullOrEmpty(password))
                {
                    appUser.PasswordHash = passwordHasher.HashPassword(appUser, password);
                }
                else
                {
                    ModelState.AddModelError("", "Password cannot be empty");
                }

                if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(appUser);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Errors(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser appUser = await userManager.FindByIdAsync(id);

            if(appUser != null)
            {
                IdentityResult result = await userManager.DeleteAsync(appUser);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View("Index", userManager.Users);
        }
        #endregion
        private void Errors(IdentityResult result)
        {
            foreach(IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
    }
}
