using bll.Interfaces;
using dal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository=new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);

        }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public int complete()
        {
            return _dbContext.SaveChanges();
        }
    }
}
