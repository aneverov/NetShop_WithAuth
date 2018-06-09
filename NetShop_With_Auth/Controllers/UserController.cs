using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetShop_With_Auth.Models;
using NetShop_With_Auth.ViewModels;

namespace NetShop_With_Auth.Controllers
{
    public class UserController : Controller
    {
        public ApplicationDbContext Context;

        public UserController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()  
        {
            IdentityUser user = await Context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            UserViewModel model = new UserViewModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            return View(model);


        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(IdentityUser user)
        {
            IdentityUser userToUpdate = await Context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            user = userToUpdate;
            //userToUpdate = Context.Users.Update(user);
            //UserViewModel model = new UserViewModel
            //{
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    UserName = user.UserName
            //};

            return RedirectToAction("ViewProfile");
        }
    }
}