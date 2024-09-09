using bll.Interfaces;
using dal.Model;
using System.Collections.Generic;
using System.Linq;
using dal.Data;
using Microsoft.EntityFrameworkCore;

namespace bll.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private  AppDbContext _DbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public int Add(Department department)
        {
            _DbContext.Department.Add(department);
            return _DbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _DbContext.Department.Remove(department);
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
            _DbContext.Department.Update(department);
            return _DbContext.SaveChanges();
        }
    }
}
