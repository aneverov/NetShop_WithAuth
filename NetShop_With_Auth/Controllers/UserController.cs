using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetShop_With_Auth.Models;
using NetShop_With_Auth.ViewModels;

namespace NetShop_With_Auth.Controllers
{
    public class UserController : Controller
    {
        public ApplicationDbContext Context;
        private readonly UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            Context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> ViewProfile()  
        {
            return View(await Context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name));
        }


        [HttpGet]
        [Authorize(Roles = "admin, user")]
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

            UserViewModel model = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> EditProfile(User user)
        {
            User userdb = Context.User.Find(user.Id);
            userdb.Name = user.Name;
            userdb.PhoneNumber = user.PhoneNumber;
            Context.Update(userdb);
            await Context.SaveChangesAsync();

            return RedirectToAction("ViewProfile", new {user.Id});
        }

        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> ChangePassword(string id)
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
            ChangePasswordModel model = new ChangePasswordModel()
            {
                Id = user.Id,
                OldPassword = String.Empty,
                Password = String.Empty,
                PasswordConfirm = String.Empty
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> ChangePassword(User user, string oldPassword, string password)
        {
            User userdb = Context.User.Find(user.Id);
            var result = await _userManager.ChangePasswordAsync(userdb, oldPassword, password);

            if (result.Succeeded)
            {
                return RedirectToAction("ViewProfile", new { Message = "Пароль успешно изменен" });
            }
            return NotFound();

        }
       
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UsersModify()
        {
            List<User> list = await Context.User.ToListAsync();

            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUserRole(string id)
        {
            User user = await Context.User.FindAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeUserRoleModel model = new ChangeUserRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

       
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUserRole(string userId, List<string> roles, bool isBlocked)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsBlocked = isBlocked;
                Context.User.Update(user);
                await Context.SaveChangesAsync();
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UsersModify");
            }
            return NotFound();
        }
    }
}