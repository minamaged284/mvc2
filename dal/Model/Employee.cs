using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace dal.Model
{

    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1,

        [EnumMember(Value = "Female")]

        Female = 2
           
    }

    public enum EmployeeType
    {
        [EnumMember(Value = "FullTime")]

        FullTime = 1,

        [EnumMember(Value = "PartTime")] 
        PartTime=2
    }
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required!")]
        [MaxLength(50,ErrorMessage ="Max length for name is 50" )]
        [MinLength(4, ErrorMessage = "Min length for name is 4")]

        public string Name { get; set; }

        [Range(21,60,ErrorMessage =("Age must be between 21 - 60"))]

        public int Age { get; set; }

        [RegularExpression(@"^[0-9]{1-3}-[a-zA-Z]{5-10}-[a-zA-Z]{4-10}-[a-zA-Z]{5-10}",ErrorMessage ="Address should be like 123-street-city-country ")]

        public string  Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireFate { get; set; }
        public bool IsDeleted { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
