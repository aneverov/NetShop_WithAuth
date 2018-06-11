using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetShop_With_Auth.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        public bool IsBlocked { get; set; }
    }
}
