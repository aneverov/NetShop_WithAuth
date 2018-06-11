using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetShop_With_Auth.Models;
using NetShop_With_Auth.ViewModels;

namespace NetShop_With_Auth.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketToPhone> BasketToPhones { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NetShop_With_Auth.Models.User> User { get; set; }

        public DbSet<NetShop_With_Auth.ViewModels.UserViewModel> UserViewModel { get; set; }

        public DbSet<NetShop_With_Auth.ViewModels.ChangePasswordModel> ChangePasswordModel { get; set; }
    }
}
