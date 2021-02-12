using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Alia.Models;

namespace Alia.TagHelpers
{
    public class PageLinkTagHelper:TagHelper
    {
        public int Page { get; set; }
        public SortViewModel SortViewModel { get; set; } 
        public string Name { get; set; }
        public int? Locality { get; set; }
        public int? Category { get; set; }
        public string Action { get; set; }  
        
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
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
            string url = urlHelper.Action(Action, new { category = Category, locality = Locality, name = Name, page = Page, sortOrder = SortViewModel.State });
            output.Attributes.SetAttribute("href", url);

        }
    }
}
