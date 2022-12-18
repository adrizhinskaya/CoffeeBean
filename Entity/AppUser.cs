using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoffeeBean.Entity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Product> WishList { get; set; }
        //public IEnumerable<string> BusketIDs { get; set; }
    }
}
