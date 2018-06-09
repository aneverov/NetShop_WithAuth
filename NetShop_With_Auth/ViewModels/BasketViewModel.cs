using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetShop_With_Auth.Models;

namespace NetShop.ViewModels
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public List<BasketToPhone> BasketToPhones { get; set; }

        public BasketViewModel()
        {
            BasketToPhones = new List<BasketToPhone>();
        }
    }
}
