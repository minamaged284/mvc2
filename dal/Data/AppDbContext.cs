using dal.Configurations;
using dal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dal.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>


    {
        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server= . ; Database=MvcProject(final); Trusted_Connection=True; MultipleActiveResultSets=True; encrypt=false ");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public IdentityUser<int> Users { get; set; }
        //public IdentityRole<int> Roles { get; set; }
    }
}
