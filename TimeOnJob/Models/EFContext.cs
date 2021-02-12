using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Alia.Models
{
    public class EFContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }

        //не только для того что бы создавались таблицы, но и для того что бы asp.net знал связи
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Employer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Employers");
            });*/
        //    modelbuilder.entity<filter>()
        //        .haskey(t => new { t.itemtype, t.itemid });

            //    modelbuilder.entity<recipe_filter>()
            //        .haskey(t => new { t.itemtype, t.itemsort });

            //    modelbuilder.entity<recipe_product>()
            //        .haskey(t => new { t.productid, t.recipeid });

            //    modelbuilder.entity<recipe_product>()
            //        .hasone(sc => sc.product)
            //        .withmany(s => s.recipe_products)
            //        .hasforeignkey(sc => sc.productid);

            //    modelbuilder.entity<recipe_product>()
            //        .hasone(sc => sc.recipe)
            //        .withmany(c => c.recipe_products)
            //        .hasforeignkey(sc => sc.recipeid);


            //    modelbuilder.entity<recipe_typeofdish>()
            //        .haskey(t => new { t.typeofdishid, t.recipeid });

            //    modelbuilder.entity<recipe_typeofdish>()
            //        .hasone(sc => sc.typeofdish)
            //        .withmany(s => s.recipe_typeofdishes)
            //        .hasforeignkey(sc => sc.typeofdishid);

            //    modelbuilder.entity<recipe_typeofdish>()
            //        .hasone(sc => sc.recipe)
            //        .withmany(c => c.recipe_typeofdishes)
            //        .hasforeignkey(sc => sc.recipeid);


            //    modelbuilder.entity<recipe_nationalcuisine>()
            //        .haskey(t => new { t.nationalcuisineid, t.recipeid });

            //    modelbuilder.entity<recipe_nationalcuisine>()
            //        .hasone(sc => sc.nationalcuisine)
            //        .withmany(s => s.recipe_nationalcuisines)
            //        .hasforeignkey(sc => sc.nationalcuisineid);

            //    modelbuilder.entity<recipe_nationalcuisine>()
            //        .hasone(sc => sc.recipe)
            //        .withmany(c => c.recipe_nationalcuisenes)
            //        .hasforeignkey(sc => sc.recipeid);
        }

        public EFContext(DbContextOptions<EFContext> options)
                : base(options)
        {
        }

    }
}