using dal.Configurations;
using dal.Model;
using Microsoft.EntityFrameworkCore;

namespace dal.Data
{
    public class AppDbContext : DbContext


    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= . ; Database=MvcProject; Trusted_Connection=True; MultipleActiveResultSets=True");
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Department>(new DepartmentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Department { get; set; } 
    }
}
