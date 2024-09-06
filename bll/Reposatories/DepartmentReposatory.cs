using bll.Interfaces;
using dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal.Data;
using Microsoft.EntityFrameworkCore;

namespace bll.Reposatories
{
    public class DepartmentReposatory : IDepartmentReposatory
    {

        private readonly AppDbContext _DbContext;

        public DepartmentReposatory(AppDbContext dbContext)
        {
            
        }
        public int Add(Department department)
        {
            _DbContext.Add(department);
            return _DbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _DbContext.Remove(department);
            return _DbContext.SaveChanges();
        }

        public List<Department> GetAll()
        {
           return _DbContext.Department.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _DbContext.Department.Find(id);
        }

        public int Update(Department department)
        {
            _DbContext.Update(department);
            return _DbContext.SaveChanges();
        }
    }
}
