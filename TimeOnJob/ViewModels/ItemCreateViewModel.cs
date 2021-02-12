using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;

namespace Alia.ViewModels
{
    public class ItemCreateViewModel
    {
        public Item Item { get; set; }
        //public IEnumerable<RecipeDetail> RecipesDetails { get; set; }
        //public IEnumerable<Recipe_Product> Recipe_Products { get; set; }
        public List<Region> Regions { get; set; }
        //public IEnumerable<Recipe_TypeOfDish> Recipe_TypeOfDishes { get; set; }
        public List<Category> Categories { get; set; }
        //public IEnumerable<Recipe_NationalCuisine> Recipe_NationalCuisenes { get; set; }
        public List<Locality> Localities { get; set; }
        public string AspNetUserName;
        public PageInfo PageInfo { get; set; }
    }
}
