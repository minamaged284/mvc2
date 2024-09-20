using AutoMapper;
using dal.Model;
using mvc2.ViewModels;

namespace mvc2.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
