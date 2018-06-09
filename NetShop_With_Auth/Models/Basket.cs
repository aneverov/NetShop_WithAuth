using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetShop_With_Auth.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public IdentityUser User { get; set; }


        public List<BasketToPhone> BasketToPhones { get; set; }

        public Basket()
        {
            BasketToPhones = new List<BasketToPhone>();
        }
    }
}
