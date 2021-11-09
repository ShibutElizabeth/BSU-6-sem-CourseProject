using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using Alia.Models;
using Alia.ViewModels;

namespace Alia.Controllers
{
    [Authorize(Roles = "admin")]
    public class ItemController : Controller
    {
        private readonly UserManager<User> _userManager;
        IWebHostEnvironment _appEnvironment;

        private EFContext db;
        public byte[] OIDC;
        public byte[] PIDC;
        public byte[] BIDC;
        public ItemController(EFContext context, UserManager<User> userManager, IConfiguration configuration, IWebHostEnvironment appEnvironment)

        {
            db = context;
            _userManager = userManager;
            Configuration = configuration;
            _appEnvironment = appEnvironment;
        }
        public IConfiguration Configuration { get; }

        

        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public async Task<IActionResult> Category()
        {
            return View(await db.Categories.ToListAsync());
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Category");
        }


        public async Task<IActionResult> CategoryEdit(int? id)
        {
            if (id != null)
            {
                Category category = await db.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CategoryEdit(Category category)
        {
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Category");
        }

        [HttpGet]
        [ActionName("CategoryDelete")]
        public async Task<IActionResult> ConfirmCategoryDelete(int? id)
        {
            if (id != null)
            {
                Category category = await db.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
                if (category != null)
                    return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id != null)
            {

                Category category = new Category { CategoryId = id.Value };
                db.Entry(category).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Category");
            }
            return NotFound();
        }

        public async Task<IActionResult> Region()
        {
            return View(await db.Regions.ToListAsync());
        }
        public IActionResult RegionCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegionCreate(Region region)
        {
            db.Regions.Add(region);
            await db.SaveChangesAsync();
            return RedirectToAction("Region");
        }


        public async Task<IActionResult> RegionEdit(int? id)
        {
            if (id != null)
            {
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.RegionId == id);
                if (region != null)
                    return View(region);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> RegionEdit(Region region)
        {
            db.Regions.Update(region);
            await db.SaveChangesAsync();
            return RedirectToAction("Region");
        }


        [HttpGet]
        [ActionName("RegionDelete")]
        public async Task<IActionResult> ConfirmRegionDelete(int? id)
        {
            if (id != null)
            {
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.RegionId == id);
                if (region != null)
                    return View(region);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RegionDelete(int? id)
        {
            if (id != null)
            {

                Region region = new Region { RegionId = id.Value };
                db.Entry(region).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Region");
            }
            return NotFound();
        }

        public async Task<IActionResult> Locality(string localityName, int page=1)
        {
            int pageSize = 8;   // количество элементов на странице

            IQueryable<Item> source = db.Items;
            
            List<Region> regions = await db.Regions.ToListAsync();
            IQueryable<Locality> localities = db.Localities;

            if (!String.IsNullOrEmpty(localityName))
            {
                localities = localities.Where(p => p.LocalityName.Contains(localityName));
            }
            var count = await localities.CountAsync();
            var items = await localities.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            LocalitiesListViewModel viewModels = new LocalitiesListViewModel
            {
                Localities = items.ToList(),
                PageViewModel = pageViewModel,
                Name = localityName
            };
            return View(viewModels);
        }

        public async Task<IActionResult> LocalityEdit(int? id)
        {
            SelectList regions = new SelectList(db.Regions, "RegionId", "RegionName");
            ViewBag.Regions = regions;

            if (id != null)
            {
                Locality locality = await db.Localities.FirstOrDefaultAsync(p => p.LocalityId == id);
                if (locality != null)
                    return View(locality);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> LocalityEdit(Locality locality)
        {
            db.Localities.Update(locality);
            await db.SaveChangesAsync();
            return RedirectToAction("Locality");
        }

        [HttpGet]
        public ActionResult LocalityCreate()
        {
            // Формируем список продуктов для передачи в представление
            SelectList regions = new SelectList(db.Regions, "RegionId", "RegionName");
            ViewBag.Regions = regions;

            return View();
        }
        [HttpPost]
        public ActionResult LocalityCreate(Locality locality)
        {
            db.Localities.Add(locality);
            db.SaveChanges();
            return RedirectToAction("Locality");
        }

        [HttpGet]
        [ActionName("LocalityDelete")]
        public async Task<IActionResult> ConfirmLocalityDelete(int? id)
        {
            if (id != null)
            {
                Locality locality = await db.Localities.FirstOrDefaultAsync(p => p.LocalityId == id);
                if (locality != null)
                    return View(locality);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> LocalityDelete(int? id)
        {
            if (id != null)
            {

                Locality locality = new Locality { LocalityId = id.Value };
                db.Entry(locality).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Locality");
            }
            return NotFound();
        }


        public ActionResult ItemCreate()
        {
            // Формируем список для передачи в представление
            SelectList localities = new SelectList(db.Localities, "LocalityId", "LocalityName");
            ViewBag.Localities = localities;
            SelectList categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult ItemCreate(IFormFile uploadedFile, ItemViewModel itemvm)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                string shortFileName = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf('\\') + 1);
                Directory.CreateDirectory(_appEnvironment.WebRootPath + "/Temp/");
                // путь к папке Temp
                string path = _appEnvironment.WebRootPath + "/Temp/";
                if (uploadedFile != null)
                {
                    // сохраняем файл в папку Temp в каталоге wwwroot
                    using (var fileStream = new FileStream(path + shortFileName, FileMode.Create))
                    {
                        uploadedFile.CopyTo(fileStream);
                    }
                    using (var imgb = Image.Load(path + shortFileName))
                    {
                        imgb.Mutate(ctx => ctx.Resize(new Size(900, 0)));
                        imgb.Save(path + "bigImage_" + _userManager.GetUserName(HttpContext.User) + "_" + shortFileName);
                        byte[] spimgb = System.IO.File.ReadAllBytes(path + "bigImage_" + _userManager.GetUserName(HttpContext.User) + "_" + shortFileName);
                        itemvm.BIData = spimgb;
                    }
                    using (var imgp = Image.Load(path + shortFileName))
                    {
                        imgp.Mutate(ctxp => ctxp.Resize(new Size(180, 0)));
                        imgp.Save(path + "previewImage_" + _userManager.GetUserName(HttpContext.User) + "_" + shortFileName);
                        byte[] spimgp = System.IO.File.ReadAllBytes(path + "previewImage_" + _userManager.GetUserName(HttpContext.User) + "_" + shortFileName);
                        itemvm.PIData = spimgp;
                    }
                    byte[] originalImageData = null;
                    using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
                    {
                        originalImageData = binaryReader.ReadBytes((int)uploadedFile.Length);
                    }
                    itemvm.OIData = originalImageData;
                    Directory.Delete(path, true);
                }
                Item itemcreate = new Item { FileName = uploadedFile.FileName.ToString() };
                itemcreate.ItemName = itemvm.ItemName;
                itemcreate.Description = itemvm.Description;
                itemcreate.OriginalImageData = itemvm.OIData;
                itemcreate.BigImageData = itemvm.BIData;
                itemcreate.PreviewImageData = itemvm.PIData;
                itemcreate.LocalityId = itemvm.LocalityId;
                itemcreate.CategoryId = itemvm.CategoryId;
                itemcreate.UserId = _userManager.GetUserId(HttpContext.User);
                itemcreate.CreatedDate = DateTime.Now;
                itemcreate.Price = itemvm.Price;
                
                
                db.Items.Add(itemcreate);
                db.SaveChanges();
            }
            return RedirectToAction("Item", "Home");
        }
        
        public async Task<IActionResult> ItemEdit(int? id)
        {
            SelectList localities = new SelectList(db.Localities.ToList(), "LocalityId", "LocalityName");
            ViewBag.Localities = localities;
            SelectList category = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            ViewBag.Categories = category;

            if (id != null)
            {
                Item item
                    = await db.Items.FirstOrDefaultAsync(p => p.ItemId == id);

                if (item != null)
                {
                    OIDC = item.OriginalImageData;
                    BIDC = item.BigImageData;
                    PIDC = item.PreviewImageData;
                    
                    return View(item);
                }
                    
            }
            return NotFound();
        }
        
        [HttpPost]
        public IActionResult ItemEdit(int? id, string ItemName, decimal? Price, string Description, int LocalityId, int CategoryId)
        {
            Item item= db.Items.FirstOrDefault(p => p.ItemId == id);
            if(ItemName != null)
            {
                item.ItemName = ItemName;
            }
            if(Price != null)
            {
                item.Price = Price;
            }
            if(Description != null)
            {
                item.Description = Description;
            }
                item.LocalityId = LocalityId;
                item.CategoryId = CategoryId;
            
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Items", "Item");
        }
        [HttpGet]
        [ActionName("ItemDelete")]
        public async Task<IActionResult> ConfirmItemDelete(int? id)
        {
            if (id != null)
            {
                Item item = await db.Items.FirstOrDefaultAsync(p => p.ItemId == id);
                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Items", "Item");
                }
                    
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ItemDelete(int? id)
        {
            List<Locality> localities = await db.Localities.ToListAsync();
            List<Category> categories = await db.Categories.ToListAsync();
            if (id != null)
            {
                
                Item item = new Item { ItemId = id.Value };
                db.Entry(item).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Items", "Item");//в отличиe от предыдущего, этот метод - оптимизированный и с проверкой на существование записи в БД
            }
            return NotFound();
        }
        
       
       
        public IActionResult Items(int? category, int? locality, string name, int? page, SortState sortOrder)
        {
            string pfl = "";

            //Динамическая загрузка списка по выбору из другого списка
            int selectedIndex = 1;
            SelectList fregions = new SelectList(db.Regions, "RegionId", "RegionName", selectedIndex);
            ViewBag.Regions = fregions;
            SelectList flocalities;
            if (selectedIndex != 1)
            {
                flocalities = new SelectList(db.Localities.Where(c => c.LocalityId == selectedIndex), "LocalityId", "LocalityName");
            }
            else
            {
                flocalities = new SelectList(db.Localities, "LocalityId", "LocalityName");
            }
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

            items = sortOrder switch
            {
                SortState.DateDesc => items.OrderByDescending(s => s.CreatedDate),
                SortState.PriceAsc => items.OrderBy(s => s.Price),
                SortState.PriceDesc => items.OrderByDescending(s => s.Price),
                SortState.DateAsc => items.OrderBy(s => s.CreatedDate),

            };
            SortViewModel sortViewModel = new SortViewModel(sortOrder);
            var count = items.Count();
            var pager = new PageInfo(count, page, pfl);
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
        public ActionResult GetLocalities(int id)
        {
            return PartialView(db.Localities.Where(c => c.RegionId == id).ToList());
        }
        public ActionResult SetReserved(int reserved, int id)
        {
            Item item = db.Items.FirstOrDefault(p => p.ItemId == id);
            if(item.IsReserved == 0)
            {
                item.IsReserved = reserved;
            }
            else
            {
                item.IsReserved = 0;
            }
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Items", "Item");
        }
        
    }
}