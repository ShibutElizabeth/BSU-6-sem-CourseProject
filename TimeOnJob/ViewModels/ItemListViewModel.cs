using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;

namespace Alia.ViewModels
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        /*public SelectList Categories { get; set; }
        public SelectList Regions { get; set; }
        public SelectList Localities { get; set; }
        public Locality Locality { get; set; }

        public string Name { get; set; }*/
        public FilterViewModel FilterViewModel { get; set; }
        public Item Item { get; set; }
        public PageInfo PageInfo { get; set; }
       public SortViewModel SortViewModel { get; set; }
    }
}
