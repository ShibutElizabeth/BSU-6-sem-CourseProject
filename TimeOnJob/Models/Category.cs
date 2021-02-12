using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Item> Items { get; set; }
        public Category()
        {
            Items = new List<Item>();
        }
    }
}
