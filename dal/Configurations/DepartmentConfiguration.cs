using dal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dal.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(p=>p.Id).UseIdentityColumn(10,10);
            builder.HasMany(p => p.Employees).WithOne(p => p.Department).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
