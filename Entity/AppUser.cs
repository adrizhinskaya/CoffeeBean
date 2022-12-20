using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoffeeBean.Entity
{
    public class AppUser : IdentityUser
    {
        public IEnumerable<Product> WishList { get; set; }
    }
}
