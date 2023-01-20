using CoffeeBean.DataAccess;
using CoffeeBean.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeBean.Controllers
{
    public class ProductsReviewController : Controller
    {
        private readonly AppIdentityDbContext dbcontext;
        public ProductsReviewController(AppIdentityDbContext context)
        {
            dbcontext = context;
        }
        public IActionResult Index(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(dbcontext.Products.Include(p => p.Cathegory));
            }
            else
            {
                var products = dbcontext.Products.Where(p => p.CategoryId == id);
                return View(products);
            }
        }
        public async Task<IActionResult> Product(string id)
        {
            Product prod = await dbcontext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return View(prod);
        }
    }
}
