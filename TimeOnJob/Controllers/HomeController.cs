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
            int selectedIndex = 0;
            Region zerorid = new Region { RegionId = 0, RegionName = "All" };
            db.Regions.Add(zerorid);
            Locality zerolid = new Locality { LocalityId = 0, LocalityName = "All", RegionId = 0 };
            db.Localities.Add(zerolid);
            SelectList fregions = new SelectList(db.Regions, "RegionId", "RegionName", selectedIndex);
            //fregions = new SelectList(new string[] { "All" });
            ViewBag.Regions = fregions;
            //new string[] {"Россия","США", "Китай","Индия"}
            //fregions.(0, new Region { RegionId = 0, RegionName = "All"});
            //ViewBag.Regions.Insert(0, new Region { RegionId = 0, RegionName = "All"});
            
            SelectList flocalities = new SelectList(db.Localities, "LocalityId", "LocalityName");
            ViewBag.Localities = flocalities;            
            IQueryable<Item> items = db.Items.Include(p => p.Category).Include(l => l.Locality);
            if (category != null && category != 0)
            {
                items = items.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Description.Contains(name));
            }
            if (locality != null && locality != 0)
            {
                items = items.Where(p => p.LocalityId == locality);
            }

            List<Category> categories = db.Categories.ToList();
            List<Locality> localities = db.Localities.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { CategoryName = "All", CategoryId = 0 });
            localities.Insert(0, new Locality { LocalityName = "All", LocalityId = 0, RegionId = 0 });

            ItemListViewModel viewModel = new ItemListViewModel
            {
                Items = items.OrderByDescending(i => i.CreatedDate).ToList(),//удалить сортировку по дате, после написания функции
                FilterViewModel = new FilterViewModel(categories, category, locality, name, localities)
                
                //Name = name
            };
            return View(viewModel);
        }
        
        public IActionResult Item(int? category, int? locality, string name, int? page, SortState sortOrder)
        {
            string pfl = "";

            //Динамическая загрузка списка по выбору из другого списка
            int selectedIndex = 0;
            Region zerorid = new Region { RegionId = 0, RegionName = "All" };
            db.Regions.Add(zerorid);
            Locality zerolid = new Locality { LocalityId = 0, LocalityName = "All", RegionId = 0 };
            db.Localities.Add(zerolid);
            SelectList fregions = new SelectList(db.Regions, "RegionId", "RegionName", selectedIndex);
            //fregions = new SelectList(new string[] { "All" });
            ViewBag.Regions = fregions;
            //new string[] {"Россия","США", "Китай","Индия"}
            //fregions.(0, new Region { RegionId = 0, RegionName = "All"});
            //ViewBag.Regions.Insert(0, new Region { RegionId = 0, RegionName = "All"});
            SelectList flocalities = new SelectList(db.Localities.Where(c => c.LocalityId == selectedIndex), "LocalityId", "LocalityName");
            
            ViewBag.Localities = flocalities;
            
            IQueryable<Item> items = db.Items.Include(p => p.Category).Include( l => l.Locality);
            
            if (category != null && category != 0)
            {
                items = items.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Description.Contains(name));
            }
            if (locality != null && locality != 0)
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
            Region zerorid = new Region { RegionId = 0, RegionName = "All" };
            db.Regions.Add(zerorid);
            Locality zerolid = new Locality { LocalityId = 0, LocalityName = "All", RegionId = 0 };
            db.Localities.Add(zerolid);
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

    }

}