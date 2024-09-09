using System.Collections.Generic;
using dal.Model;


namespace bll.Interfaces
{
    public interface IDepartmentRepository
    {

        List<Department> GetAll();
        Department GetById(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);


    }
}
