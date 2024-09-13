using dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase

    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T item);
        int Update(T item);
        int Delete(T item);


    }
}
