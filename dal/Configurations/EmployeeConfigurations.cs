using dal.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dal.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.Property(e=>e.Gender).HasConversion(gender=>gender.ToString(),genderDb=>Enum.Parse<Gender>(genderDb));
            builder.Property(e => e.Gender).HasConversion(gender => gender.ToString(), genderDb =>(Gender)Enum.Parse(typeof(Gender),genderDb));

            //builder.Property(e=>e.EmployeeType).HasConversion(employeeType=>employeeType.ToString(),employeeTypeDb=>Enum.Parse<EmployeeType>(employeeTypeDb));
            builder.Property(e => e.EmployeeType).HasConversion(employeeType => employeeType.ToString(), employeeTypeDb => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeTypeDb));

            builder.Property(e => e.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
