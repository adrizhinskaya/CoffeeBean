using CoffeeBean.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
                        CategoryImg = System.Text.Encoding.Default.GetBytes("0xFFD8FFE100CA4578696600004D4D002A000000080007011200030000000100010000011A00050000000100000062011B0005000000010000006A012800030000000100020000021300030000000100010000829800020000000E00000072876900040000000100000080000000000000012C000000010000012C000000015261")
                    },
                    new Category
                    {
                        Name = "Syrups & Sweeteners",
                        Description = "Combining art, craftsmanship and science, syrups and sweeteners create countless masterpiece recipes and bring a unique palette of flavors to your traditional drink menu",
                        CategoryImg = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010100000100010000FFDB0043000503040404030504040405050506070C08070707070F0B0B090C110F1212110F111113161C1713141A1511111821181A1D1D1F1F1F13172224221E241C1E1F1EFFDB0043010505050706070E08080E1E1411141E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E1E")
                    },
                    new Category
                    {
                        Name = "Utensil",
                        Description = "Coffee utensils are not just a part of the interior or corporate identity of a coffee shop. A cup can enhance the perception of the taste of a drink: emphasize its strengths or weaknesses - bright acidity, pronounced sweetness or bitterness.",
                        CategoryImg = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010100012C012C0000FFE102E44578696600004D4D002A000000080006010F0002000000060000005601100002000000140000005C011A00050000000100000070011B000500000001000000780132000200000014000000808769000400000001000000940000000043616E6F6E0043616E6F6E20")
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
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Coffee").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "70% Arabica & 30% Robusta",
                        Description = "70% Arabica and 30% Robusta - fans of an invigorating, rich taste will appreciate them. The blend is perfect for drinks with the addition of milk.",
                        Size = "1000g",
                        Price = 23.39m,
                        Count = 15,
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Coffee").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Syrup \"Peach\"",
                        Description = "Peach syrup perfectly repeats the taste of sweet and fragrant fruits, and can give unique peach shades all year round to both confectionery and soft drinks, and alcoholic cocktails",
                        Size = "1000ml",
                        Price = 25.60m,
                        Count = 10,
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Syrups & Sweeteners").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Syrup \"Maple\"",
                        Description = "Maple syrup tastes as sweet as sugar. It has a unique aroma, a pleasant aftertaste and a beautiful color - from light golden to rich amber.",
                        Size = "1000ml",
                        Price = 20.60m,
                        Count = 10,
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Syrups & Sweeteners").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Chemex",
                        Description = "Chemex borosilicate glass for brewing coffee with a volume of 400 ml",
                        Size = "600ml",
                        Price = 50.55m,
                        Count = 8,
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Utensil").FirstOrDefault().Id
                    },
                    new Product
                    {
                        Name = "Cup",
                        Description = "A stylish ceramic cup will attract attention with its modern design and pleasant colors",
                        Size = "250ml",
                        Price = 6.77m,
                        Count = 8,
                        Image = System.Text.Encoding.Default.GetBytes("0xFFD8FFE000104A46494600010101004800480000FFE202284943435F50524F46494C450001010000021800000000043000006D6E74725247422058595A2000000000000000000000000061637370000000000000000000000000000000000000000000000000000000010000F6D6000100000000D32D00000000000000000000"),
                        CategoryId = context.Categories.Where(c => c.Name == "Utensil").FirstOrDefault().Id
                    });

                await context.SaveChangesAsync();
            }
        }
    }
}
