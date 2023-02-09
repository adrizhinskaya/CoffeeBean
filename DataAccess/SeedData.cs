using CoffeeBean.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeBean.DataAccess
{
    class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppIdentityDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppIdentityDbContext>>()))
            {
                if (context.Categories.Any())
                {
                    return;
                }
                await context.Categories.AddRangeAsync(
                    new Category
                    {
                        Name = "Coffee",
                        Description = "What is the benefit of coffee beans while observing the measure of consumption?  May reduce the risk of coronary disease. The fact is that the drink reduces the oxidation of low-density lipoprotein cholesterol. Studies have shown that drinking one to five drinks a day reduces the risk of stroke by 22-25%. These are very positive forecasts. The risk of developing malignant tumors of certain types becomes lower. This includes melanoma, basal cell carcinoma, formations in the mammary glands, endometrium, and so on. This is due to the antioxidant properties of grains. Diabetes prevention is another health benefit of coffee beans. Regular consumption of the drink improves glucose metabolism and insulin secretion, which significantly reduces the risk of this unpleasant disease.",
                        CategoryImg = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\category_coffee.jpg")
                    },
                    new Category
                    {
                        Name = "Syrups & Sweeteners",
                        Description = "Combining art, craftsmanship and science, syrups and sweeteners create countless masterpiece recipes and bring a unique palette of flavors to your traditional drink menu",
                        CategoryImg = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\category_syrups.jpg")
                    },
                    new Category
                    {
                        Name = "Utensil",
                        Description = "Coffee utensils are not just a part of the interior or corporate identity of a coffee shop. A cup can enhance the perception of the taste of a drink: emphasize its strengths or weaknesses - bright acidity, pronounced sweetness or bitterness.",
                        CategoryImg = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\category_utensil.jpg")
                    }
                );
                await context.SaveChangesAsync();

                if (context.Products.Any())
                {
                    return;
                }
                await context.Products.AddRangeAsync(
                    new Product
                    {
                        Name = "Arabica",
                        Description = "The main type of coffee, has a pronounced taste and aroma, which is fully revealed during the roasting process. Arabica is superior to other types of coffee in terms of export volume and consumption demand. Arabica coffee trees are very sensitive to weather conditions and diseases, so coffee growers are constantly breeding to develop new varieties of coffee trees that will favorably tolerate climate change. As a result of Arabica mutations, about 50 of its varieties appeared.",
                        Size = "1000g",
                        Price = 34.99m,
                        Count = 15,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_coffeeArabica.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Coffee").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "70% Arabica & 30% Robusta",
                        Description = "70% Arabica and 30% Robusta - fans of an invigorating, rich taste will appreciate them. The blend is perfect for drinks with the addition of milk.",
                        Size = "1000g",
                        Price = 23.39m,
                        Count = 15,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_coffee5050.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Coffee").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Syrup \"Cherry\"",
                        Description = "Cherry syrup perfectly repeats the taste of sweet and fragrant fruits, and can give unique peach shades all year round to both confectionery and soft drinks, and alcoholic cocktails",
                        Size = "1000ml",
                        Price = 25.60m,
                        Count = 10,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_syrupCherry.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Syrups & Sweeteners").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Syrup \"Maple\"",
                        Description = "Maple syrup tastes as sweet as sugar. It has a unique aroma, a pleasant aftertaste and a beautiful color - from light golden to rich amber.",
                        Size = "1000ml",
                        Price = 20.60m,
                        Count = 10,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_syrupMaple.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Syrups & Sweeteners").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Chemex",
                        Description = "Chemex borosilicate glass for brewing coffee with a volume of 400 ml",
                        Size = "600ml",
                        Price = 50.55m,
                        Count = 8,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_chemex.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Utensil").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Cup",
                        Description = "A stylish ceramic cup will attract attention with its modern design and pleasant colors",
                        Size = "250ml",
                        Price = 6.77m,
                        Count = 8,
                        Image = File.ReadAllBytes(@"C:\Users\Alina\Записанные курсы\проект1\CoffeeBean\wwwroot\images\product_cup.jpg"),
                        CategoryId = context.Categories.Where(c => c.Name == "Utensil").FirstOrDefault().Id
                    });

                await context.SaveChangesAsync();
            }
        }
    }
}
