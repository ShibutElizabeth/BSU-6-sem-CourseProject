using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;

namespace Alia.ViewModels
{
    public class LocalitiesListViewModel
    {
        public IEnumerable<Locality> Localities { get; set; }
        public SelectList Regions { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
