using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetShop_With_Auth.Models;

namespace NetShop.ViewModels
{
    public class PhoneDetailsViewModel
    {
        public Phone Phone { get; set; }
        public CommentsViewModel CommentsViewModel { get; set; }
    }
}
