using AirLineAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLineLibrary
{
    public  class AirDbContext : DbContext
    {
            public AirDbContext()
            {

            }
          public AirDbContext(DbContextOptions options) : base(options)
          { 
        
           }
            public DbSet<AirLine> AirLines { get; set; }
            

            protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
            {
                OptionsBuilder.UseSqlServer(@"Server=DESKTOP-2PKBUH0\SQLEXPRESS;Database=AirLinesDb;Trusted_Connection=True;");
            }

            protected override void OnModelCreating(ModelBuilder builder)
           {
              builder.Entity<AirLine>().HasIndex(u => u.AirLineName).IsUnique();
           }
     }
}

