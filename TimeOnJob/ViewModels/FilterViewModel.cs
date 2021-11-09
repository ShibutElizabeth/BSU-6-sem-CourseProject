using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using Alia.Models;

namespace Alia.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categories, int? category, int? locality, string name, List<Locality> localities)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            Categories = new SelectList(categories, "CategoryId", "CategoryName");
            Localities = new SelectList(localities, "LocalityId", "LocalityName");
            Name = name;
            SelectedCategory = category;
            SelectedLocality = locality;
        }
        public SelectList Categories { get; set; }
        public SelectList Localities { get; set; }
        public string Name { get; set; }
        public int? SelectedCategory { get; set; }
        public int? SelectedLocality { get; set; }

    }
}
