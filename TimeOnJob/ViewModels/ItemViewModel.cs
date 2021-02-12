using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;

namespace Alia.ViewModels
{
    public class ItemViewModel
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int LocalityId { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public IFormFile OID { get; set; }
        public IFormFile BID { get; set; }
        public IFormFile PID { get; set; }
        public byte[] OIData { get; set; }
        public byte[] BIData { get; set; }
        public byte[] PIData { get; set; }
        public SelectList Localities { get; set; }
        public SelectList Categories { get; set; }
        public Item Item { get; set; }
        //public IEnumerable<RecipeDetail> RecipesDetails { get; set; }
        //public IEnumerable<Recipe_Product> Recipe_Products { get; set; }
        //public List<Region> Regions { get; set; }//аналог  public SelectList Regions { get; set; }
        //public IEnumerable<Recipe_TypeOfDish> Recipe_TypeOfDishes { get; set; }
        //public List<Category> Categories { get; set; }
        //public IEnumerable<Recipe_NationalCuisine> Recipe_NationalCuisenes { get; set; }
        //public List<Locality> Localities { get; set; }
        //public string AspNetUserName;
        //public PageInfo PageInfo { get; set; }
    }
}
