using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace mvc2.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "name is required")]

        public string Name { get; set; }


        [Display(Name = "Date Of Creation")]
        public DateTime? DateOfCreation { get; set; }

    }
}
