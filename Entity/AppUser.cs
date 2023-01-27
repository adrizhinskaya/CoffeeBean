using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoffeeBean.Entity
{
    public class AppUser : IdentityUser
    {
        public List<Product> Cart { get; set; } = new List<Product>();
    }
}
