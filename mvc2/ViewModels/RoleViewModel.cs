using System;
using System.ComponentModel.DataAnnotations;

namespace mvc2.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name ="Name")]
        public string RoleName { get; set; }
        public RoleViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }
    }
}
