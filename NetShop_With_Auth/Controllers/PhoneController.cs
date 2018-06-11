using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetShop.ViewModels;
using NetShop_With_Auth.Models;

namespace NetShop_With_Auth.Controllers
{
    public class PhoneController : Controller
    {
        private ApplicationDbContext context;

        public PhoneController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(
            int? company,
            string name,
            int? priceFrom,
            int? priceTo,
            bool exist,
            SortState sortOrder = SortState.NameAsc,
            int page = 1)
        {
            IndexViewModel model = new IndexViewModel();
            IQueryable<Phone> phones = context.Phones.Include(p => p.Company);

            if (company != null && company != 0)
            {
                phones = phones.Where(p => p.CompanyId == company);
            }

            if (!string.IsNullOrEmpty(name))
            {
                phones = phones.Where(p => p.Name.Contains(name));
            }

            if (priceFrom != null)
            {
                phones = phones.Where(p => p.Price >= priceFrom);
            }

            if (priceTo != null)
            {
                phones = phones.Where(p => p.Price <= priceTo);
            }

            if (exist)
            {
                phones = phones.Where(p => p.Quantity > 0);
            }

            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            ViewBag.CompSort = sortOrder == SortState.CompAsc ? SortState.CompDesc : SortState.CompAsc;

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    phones = phones.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    phones = phones.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    phones = phones.OrderByDescending(s => s.Price);
                    break;
                case SortState.CompAsc:
                    phones = phones.OrderBy(s => s.Company.Name);
                    break;
                case SortState.CompDesc:
                    phones = phones.OrderByDescending(s => s.Company.Name);
                    break;
                default:
                    phones = phones.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int count = phones.Count();

            phones = phones.Skip((page - 1) * pageSize).Take(pageSize);


            model.Phones = phones.ToList();
            model.Companies = new SelectList(context.Companies.ToList(), "Id", "Name");
            model.Name = name;
            model.PriceFrom = priceFrom;
            model.PriceTo = priceTo;
            model.Exist = exist;
            model.CompanyId = company;
            model.PageViewModel = new PageViewModel(count, page, pageSize);

            return View(model);


        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Create()
        {
            List<Company> companies = context.Companies.ToList();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Create(Phone phone)
        {
            context.Phones.Add(phone);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Phone phone = context.Phones.Include(p => p.Comments).Include(p => p.Company).FirstOrDefault(p => p.Id == id);
            PhoneDetailsViewModel model = new PhoneDetailsViewModel()
            {
                Phone = phone,
                CommentsViewModel = new CommentsViewModel()
                {
                    PhoneId = phone.Id,
                    Comments = phone.Comments,
                    NewComment = new Comment() { PhoneId = phone.Id }
                }
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Buy(int id, int quantity)
        {
            Phone phone = context.Phones.FirstOrDefault(p => p.Id == id);
            phone.Quantity -= quantity;
            context.SaveChanges();
            return RedirectToAction("Details", "Phone", new { id });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Comment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            context.Comments.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Details", "Phone", new { id = comment.PhoneId });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Phone phone = context.Phones.FirstOrDefault(p => p.Id == id);
            return View(phone);
        }

        [Authorize(Roles = "admin")]

        [HttpPost]
        public IActionResult Edit(Phone phone)
        {
            Phone phoneToUpdate = context.Phones.First(p => p.Id == phone.Id);
            phone = phoneToUpdate;
            context.Phones.Update(phone);
            context.SaveChanges();

            return RedirectToAction("Details", "Phone", new { phone.Id });
        }

        [Authorize(Roles = "admin")]

        public IActionResult Delete(int id)
        {
            Phone phone = context.Phones.FirstOrDefault(p => p.Id == id);
            return View(phone);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Delete(Phone phone)
        {
            context.Phones.Remove(phone);
            context.SaveChanges();

            return RedirectToAction("Index", "Phone");
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteComment(int id, int phoneId)
        {
            Comment comment = context.Comments.FirstOrDefault(c => c.Id == id);
            context.Comments.Remove(comment);
            context.SaveChanges();

            return RedirectToAction("Details", "Phone", new { id = phoneId });
        }

        ////[Authorize(Roles = "user")]
        //public IActionResult Basket()
        //{
        //    User user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
        //    Basket basket = context.Baskets.FirstOrDefault(p => p.UserId == user.Id);

        //    List<BasketToPhone> list = context.BasketToPhones.Where(b => b.BasketId == basket.Id).Include(p => p.Phone).ToList();

        //    BasketViewModel model = new BasketViewModel { User = user, BasketToPhones = list };

        //    return View(model);
        //}

        [Authorize(Roles = "user")]
        [HttpPost]
        public IActionResult Basket(int id)
        {
            User user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            Basket basket = context.Baskets.FirstOrDefault(p => p.UserId == user.Id);
            if (basket == null)
            {
                basket = new Basket() { UserId = user.Id };
                context.Baskets.Add(basket);
            }
            BasketToPhone mod = new BasketToPhone
            {
                PhoneId = id,
                BasketId = basket.Id
            };
            context.BasketToPhones.Add(mod);
            context.SaveChanges();
            List<BasketToPhone> list = context.BasketToPhones.Where(b => b.BasketId == basket.Id).Include(p => p.Phone).ToList();

            BasketViewModel model = new BasketViewModel { User = user, BasketToPhones = list };

            return View(model);
        }

        [Authorize(Roles = "user")]
        public IActionResult DeletefromBasket(int id)
        {
            User user = context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            Basket basket = context.Baskets.FirstOrDefault(p => p.UserId == user.Id);

            BasketToPhone mod = context.BasketToPhones.Where(bp => bp.BasketId == basket.Id).FirstOrDefault(bp => bp.PhoneId == id);

            context.BasketToPhones.Remove(mod);
            context.SaveChanges();

            List<BasketToPhone> list = context.BasketToPhones.Where(b => b.BasketId == basket.Id).Include(p => p.Phone).ToList();

            BasketViewModel model = new BasketViewModel { User = user, BasketToPhones = list };

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckName(string name, int companyId)
        {
            Phone phone = context.Phones.FirstOrDefault(p => p.Name == name && p.CompanyId == companyId);
            return Json(phone == null);
        }
    }
}