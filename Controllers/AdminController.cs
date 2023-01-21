using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using CoffeeBean.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateCategory(Category category, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                //category.Image = imageData;
            }
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
        public async Task<IActionResult> UpdateCategory(Category category, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                //category.Image = imageData;
            }
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
            return View(dbcontext.Products.Include(p => p.Cathegory));
        }
        public ViewResult CreateProduct()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            categories = dbcontext.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.Category = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                product.Image = imageData;
            }

            await dbcontext.Products.AddAsync(product);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Products");
        }
        public async Task<IActionResult> UpdateProduct(string id)
        {
            Product prod = await dbcontext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

            List<SelectListItem> categories = new List<SelectListItem>();
            categories = dbcontext.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.Category = categories;

            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                }
                product.Image = imageData;
            }

            dbcontext.Products.Update(product);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Products");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var prod = new Product() { Id = id };
            dbcontext.Remove(prod);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("Products");
        }
        #endregion

        #region UserManaging
        public IActionResult Users()
        {
            return View(userManager.Users);
        }

        public ViewResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
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
                    return RedirectToAction("Users");
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

        public async Task<IActionResult> UpdateUser(string id)
        {
            AppUser appUser = await userManager.FindByIdAsync(id);

            if(appUser != null)
            {
                return View(appUser);
            }
            else
            {
                return RedirectToAction("Users");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, string email, string password)
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
                        return RedirectToAction("Users");
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
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser appUser = await userManager.FindByIdAsync(id);

            if(appUser != null)
            {
                IdentityResult result = await userManager.DeleteAsync(appUser);
                if(result.Succeeded)
                {
                    return RedirectToAction("Users");
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

            return View("Users", userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach(IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
        #endregion
    }
}
