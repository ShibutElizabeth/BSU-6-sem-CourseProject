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

        
        public EFContext(DbContextOptions<EFContext> options)
                : base(options)
        {
        }

        //не только для того что бы создавались таблицы, но и для того что бы asp.net знал связи
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}