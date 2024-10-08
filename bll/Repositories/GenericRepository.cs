﻿using bll.Interfaces;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _DbContext;

        public GenericRepository(AppDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public void Add(T item)
        {
           _DbContext.Set<T>().Add(item);
            //return _DbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            _DbContext.Set<T>().Remove(item);
            //return _DbContext.SaveChanges();
        }

        public T GetById(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>)_DbContext.Employees.Include(e => e.Department).ToList();
            }
            return _DbContext.Set<T>().ToList();
        }

        public void Update(T item)
        {
            _DbContext.Set<T>().Update(item);

            //return _DbContext.SaveChanges();
        }
    }
}
