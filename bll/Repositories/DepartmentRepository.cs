using bll.Interfaces;
using dal.Model;
using System.Collections.Generic;
using System.Linq;
using dal.Data;
using Microsoft.EntityFrameworkCore;

namespace bll.Repositories
{
    public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext):base(dbContext) { }


    }
}
