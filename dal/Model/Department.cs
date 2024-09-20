using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dal.Model
{
    public class Department:ModelBase
    {

        [Required]
        public string Code { get; set; }

        [Required]

        public string Name { get; set; }


        public DateTime? DateOfCreation{ get; set; }

        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();

    }
}
