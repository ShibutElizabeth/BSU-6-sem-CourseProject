using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Alia.Models;
using Alia.ViewModels;

namespace Alia.TagHelpers
{
    public class SortLabelTagHelper:TagHelper
    {
        public SortState Property { get; set; } // значение текущего свойства, для которого создается тег
        public SortState Current { get; set; }  // значение активного свойства, выбранного для сортировки
       
        public string Name { get; set; }
        public int? Locality { get; set;}
        public int? Category { get; set; }
        public string Action { get; set; }  // действие контроллера, на которое создается ссылка
        public bool UpPrice { get; set; }    // сортировка по возрастанию или убыванию
        public bool UpDate { get; set; }    // сортировка по возрастанию или убыванию
       
        private IUrlHelperFactory urlHelperFactory;

        public SortLabelTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "a";
            string url = urlHelper.Action(Action, new { category = Category, locality = Locality, name = Name, page = 1,sortOrder = Property});
            output.Attributes.SetAttribute("href", url);
            // если текущее свойство имеет значение CurrentSort
            if (Current == Property)
            {
                TagBuilder tag = new TagBuilder("i");
                
                tag.AddCssClass("glyphicon");
                    if (UpPrice == true || UpDate==true)   // если сортировка по возрастанию
                        tag.AddCssClass("glyphicon-chevron-up");
                    else   // если сортировка по убыванию
                        tag.AddCssClass("glyphicon-chevron-down");
                output.PreContent.AppendHtml(tag);
            }
        }
    }
}
