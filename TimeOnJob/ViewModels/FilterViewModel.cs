﻿using System;
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
        public FilterViewModel(List<Category> categories, int? category, int? locality, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { CategoryName = "All", CategoryId = 0 });
            //localities.Insert(0, new Locality { LocalityName = "All", LocalityId = 0, RegionId = 0 });
            Categories = new SelectList(categories, "CategoryId", "CategoryName");
            Name = name;
            SelectedCategory = category;
            SelectedLocality = locality;
        }
        public SelectList Categories { get; set; }
        public string Name { get; set; }
        public int? SelectedCategory { get; set; }
        public int? SelectedLocality { get; set; }

    }
}