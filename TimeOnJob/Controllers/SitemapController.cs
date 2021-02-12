using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alia.Controllers
{
    public class SitemapController : Controller
    {
        private readonly EFContext db;
        public SitemapController(EFContext context)
        {
            db = context;
        }

        //[Route("sitemap.xml")]
        //public actionresult sitemap()
        //{
        //    string baseurl = "http://timeonjob.eu/";

        //    // get a list of published articles
        //    var recipes = db.recipes.tolist();

        //    // get last modified date of the home page
        //    var sitemapbuilder = new sitemapbuilder();

        //    // add the home page to the sitemap
        //    sitemapbuilder.addurl(baseurl, modified: datetime.utcnow, changefrequency: changefrequency.weekly, priority: 1.0);

        //    sitemapbuilder.addurl(baseurl + "home/contact", modified: datetime.utcnow, changefrequency: null, priority: 0.5);

        //    sitemapbuilder.addurl(baseurl + "home/about", modified: datetime.utcnow, changefrequency: null, priority: 0.5);

        //    // add details recipe to the sitemap
        //    foreach (var recipe in recipes)
        //    {
        //        sitemapbuilder.addurl(baseurl + "home/detailsrecipe/" + recipe.recipeid, modified: recipe.createddate, changefrequency: null, priority: 0.5);
        //    }

        //    // generate the sitemap xml
        //    string xml = sitemapbuilder.tostring();
        //    return content(xml, "text/xml");
        //}
    }
}