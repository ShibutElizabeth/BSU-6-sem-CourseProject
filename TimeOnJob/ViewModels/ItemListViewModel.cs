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
        
        public FilterViewModel FilterViewModel { get; set; }
        
        public Item Item { get; set; }
        public PageInfo PageInfo { get; set; }
        public PageViewModel PageViewModel { get; set; }
       public SortViewModel SortViewModel { get; set; }
    }
}
