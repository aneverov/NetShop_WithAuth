using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetShop_With_Auth.Models;

namespace NetShop
{
    public class ModelData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Companies.Any())
            {
                context.Companies.AddRange
                    (
                        new Company() { Name = "Samsung"},
                        new Company() { Name = "Xiaomi"},
                        new Company() { Name = "Apple"}
                    );
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange
                    (
                        new IdentityRole() { Name = "admin" },
                        new IdentityRole() { Name = "user" }
                    );
            }
            context.SaveChanges();
            if (!context.Phones.Any())
            {
                context.Phones.AddRange
                    (
                        new Phone()
                        {
                            Name = "S8",
                            CompanyId = context.Companies.FirstOrDefault(c => c.Name == "Samsung").Id,
                            Price = 900
                        },
                        new Phone()
                        {
                            Name = "Iphone X",
                            CompanyId = context.Companies.FirstOrDefault(c => c.Name == "Apple").Id,
                            Price = 1000
                        },
                        new Phone()
                        {
                            Name = "Mi7",
                            CompanyId = context.Companies.FirstOrDefault(c => c.Name == "Xiaomi").Id,
                            Price = 500
                        }
                    );
                context.SaveChanges();
            }
        }
    }
}
