using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Alia.Models;

namespace Alia.TagHelpers
{
    public class PageLinkSecTagHelper:TagHelper
    {
        public int Page { get; set; }
        public string Action { get; set; }  
        
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkSecTagHelper(IUrlHelperFactory helperFactory)
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
            string url = urlHelper.Action(Action, new { page = Page});
            output.Attributes.SetAttribute("href", url);

        }
    }
}
