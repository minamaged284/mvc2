using AutoMapper;
using dal.Model;
using Microsoft.AspNetCore.Identity;
using mvc2.ViewModels;

namespace mvc2.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
            CreateMap<UserViewModel,ApplicationUser >().ForMember(U => U.UserName, opt => opt.Ignore()).ReverseMap();
            CreateMap<RoleViewModel,IdentityRole >().ForMember(r=>r.Name,d=>d.MapFrom(s=>s.RoleName)).ReverseMap();

        }
    }
}
