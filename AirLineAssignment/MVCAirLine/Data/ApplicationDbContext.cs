using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCAirLine.Models;

namespace MVCAirLine.Data
{
    public class ApplicationDbContext : IdentityDbContext<Field>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         public DbSet<MVCAirLine.Models.AirViewModel>? AirViewModel { get; set; }
         public DbSet<MVCAirLine.Models.AdminPage>? AdminPage { get; set; }

       // public DbSet<AdminRole> Register { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-2PKBUH0\SQLEXPRESS;Database=AirLinesDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminPage>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.ApplyConfiguration(new AplicationUserEntityConfiguration());
        }
        public class AplicationUserEntityConfiguration : IEntityTypeConfiguration<Field>
        {
            public void Configure(EntityTypeBuilder<Field> builder)
            {
                builder.Property(p => p.PanNo).HasMaxLength(10);
            }
            
        }
    }
}
    


