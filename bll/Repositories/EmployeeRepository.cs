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
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _DbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            this._DbContext = dbContext;
        }
        public int Add(Employee employee)
        {
            _DbContext.Employees.Add(employee);
            return _DbContext.SaveChanges();

        }

        public int Delete(Employee employee)
        {
            _DbContext.Employees.Remove(employee);
            return _DbContext.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return _DbContext.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _DbContext.Employees.Find(id);


        }

        public int Update(Employee employee)
        {
            _DbContext.Employees.Update(employee);
            return _DbContext.SaveChanges();
        }
    }
}