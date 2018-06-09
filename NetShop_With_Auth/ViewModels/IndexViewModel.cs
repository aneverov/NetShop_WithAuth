using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetShop_With_Auth.Models;

namespace NetShop.ViewModels
{
    public class IndexViewModel
    {
        public SelectList Companies { get; set; }
        public string Name { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? CompanyId { get; set; }
        public bool Exist { get; set; }

        public List<Phone> Phones { get; set; }
        public PageViewModel PageViewModel { get; set; }


    }
}
