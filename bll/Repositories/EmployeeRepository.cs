using bll.Interfaces;
using dal.Data;
using dal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext):base(dbContext) { }
       
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _DbContext.Employees.Where(e=>e.Address.ToLower().Contains(address.ToLower()));
        }
    }
}