using System;
using System.ComponentModel.DataAnnotations;

namespace dal.Model
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "name is required")]

        public string Name { get; set; }


        [Display(Name="Date Of Creation")]
        public DateTime? DateOfCreation{ get; set; }

    }
}
