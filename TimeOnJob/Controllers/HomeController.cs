using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using Alia.Models;
using Alia.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Alia.Controllers
{
    public class HomeController : Controller
    {
        private EFContext db;
        public HomeController(EFContext context)
        {
            db = context;
        }

        public IActionResult Index(int? category, int? locality, string name)
        {
            
            //Динамическая загрузка списка по выбору из другого списка
            int selectedIndex = 1;
            
            SelectList fregions = new SelectList(db.Regions, "RegionId", "RegionName", selectedIndex);
            ViewBag.Regions = fregions;
            
            SelectList flocalities = new SelectList(db.Localities, "LocalityId", "LocalityName");
            ViewBag.Localities = flocalities;            
            IQueryable<Item> items = db.Items.Include(p => p.Category).Include(l => l.Locality);
            if (category != null && category != 1)
            {
                items = items.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Description.Contains(name));
            }
            if (locality != null && locality != 1)
            {
                items = items.Where(p => p.LocalityId == locality);
            }

            List<Category> categories = db.Categories.ToList();
            List<Locality> localities = db.Localities.ToList();
            
            ItemListViewModel viewModel = new ItemListViewModel
            {
                Items = items.OrderByDescending(i => i.CreatedDate).ToList(),
                FilterViewModel = new FilterViewModel(categories, category, locality, name, localities)
            };
            return View(viewModel);
        }
        
        public IActionResult Item(int? category, int? locality, string name, int? page, SortState sortOrder)
        {
            string pfl = "";

            //Динамическая загрузка списка по выбору из другого списка
            int selectedIndex = 1;
            SelectList fregions = new SelectList(db.Regions, "RegionId", "RegionName", selectedIndex);
            ViewBag.Regions = fregions;
            SelectList flocalities = new SelectList(db.Localities.Where(c => c.LocalityId == selectedIndex), "LocalityId", "LocalityName");
            ViewBag.Localities = flocalities;
            
            IQueryable<Item> items = db.Items.Include(p => p.Category).Include( l => l.Locality);
            
            if (category != null && category != 1)
            {
                items = items.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Description.Contains(name));
            }
            if (locality != null && locality != 1)
            {
                items = items.Where(p => p.LocalityId == locality);
            }
            
            items = sortOrder switch
            {
                SortState.DateDesc => items.OrderByDescending(s => s.CreatedDate),
                SortState.PriceAsc => items.OrderBy(s => s.Price),
                SortState.PriceDesc => items.OrderByDescending(s => s.Price),
                SortState.DateAsc => items.OrderBy(s => s.CreatedDate),
                
            };
            SortViewModel sortViewModel = new SortViewModel(sortOrder);
            var count = items.Count();
            var pager = new PageInfo(count, page, pfl, 6);
            var keys = items.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();


            ItemListViewModel viewModel = new ItemListViewModel
            {
                SortViewModel = sortViewModel,
                FilterViewModel = new FilterViewModel(db.Categories.ToList(), category, locality, name, db.Localities.ToList()),
                Items = keys,
                PageInfo = pager
            };
            
            return View(viewModel);
        }
        public ActionResult GetItems(int id)
        {
            return PartialView(db.Localities.Where(c => c.RegionId == id).ToList());
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //////////////////////test upload
        public ActionResult ItemCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ItemCreate(IFormFile uploadedFile, Item item)
        {
            using var image = Image.Load(uploadedFile.OpenReadStream());
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> ItemDetails(int? id)
        {
            Item item = await db.Items.FirstOrDefaultAsync(p => p.ItemId == id);
            if (item == null)
            {
                return BadRequest("No such order found for this user.");
            }
            List<Locality> localities = await db.Localities.ToListAsync();
            List<Category> categories = await db.Categories.ToListAsync();
            return View(item);
        }
        public async Task<IActionResult> ItemReserve(int? id, string userId)
        {
            SelectList localities = new SelectList(db.Localities.ToList(), "LocalityId", "LocalityName");
            ViewBag.Localities = localities;
            SelectList category = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            ViewBag.Categories = category;

            

            if (id != null && userId != null)
            {
                Item item
                    = await db.Items.FirstOrDefaultAsync(p => p.ItemId == id);
                AspNetUser user = await db.AspNetUsers.FirstOrDefaultAsync(p => p.Id == userId);
                if (item != null && user != null)
                {
                    MailReserve mailReserve = new MailReserve
                    {
                        ItemId = id,
                        ItemName = item.ItemName

                    };
                    ItemReserveViewModel itemReserveViewModel = new ItemReserveViewModel
                    {
                        User = user,
                        Item = item,
                        MailReserve = mailReserve
                    };
                    return View(itemReserveViewModel);
                }
            }
            return NotFound();
        }
        
        [HttpPost]
        public IActionResult ItemReserve(MailReserve mailReserve)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("beautykoteyko@gmail.com", mailReserve.UserName);
            // кому отправляем
            MailAddress to = new MailAddress("ellis.shybut@gmail.com");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Booking Request";
            // текст письма
            string firstString = "item: " + mailReserve.ItemName + " " + mailReserve.ItemId.ToString() + "\n";
            string secondString = "from: " + mailReserve.UserName + " " + mailReserve.UserId + "\n";
            string thirdString = "phone number: " + mailReserve.PhoneNumber + "\n";
            string fourthString = "message: " + mailReserve.Message + "\n";
            m.Body = firstString + secondString + thirdString + fourthString;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("beautykoteyko@gmail.com", "6739396koteyko");
            smtp.EnableSsl = true;
            smtp.Send(m);
            return RedirectToAction("Item", "Home");
        }
        
    }

}