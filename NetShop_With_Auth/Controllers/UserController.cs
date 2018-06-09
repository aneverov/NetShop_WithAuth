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
            //User user = ;
            

            return View(await Context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name));


        }


        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(User user)
        {
            User TempUser = new User
            {

                NormalizedEmail = user.Email.ToUpper(),
                NormalizedUserName = user.UserName.ToUpper(),

            };
            user.NormalizedEmail = TempUser.NormalizedEmail;
            user.NormalizedUserName = TempUser.NormalizedUserName;
            Context.Update(user);
            await Context.SaveChangesAsync();
            return View(user);
        }
    }
}