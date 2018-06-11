using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NetShop_With_Auth.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public override string Email { get; set; }
        public bool IsBlocked { get; set; }
    }
}
