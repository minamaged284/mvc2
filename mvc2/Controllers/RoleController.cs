using AutoMapper;
using dal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc2.Helpers;
using mvc2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc2.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var users = await _roleManager.Roles.Select(u => new RoleViewModel
                {
                    Id = u.Id,
                    RoleName = u.Name,
                    
                }).ToListAsync();
                return View(users);
            }

            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                if (role != null)
                {
                    var mappedRole = new RoleViewModel
                    {

                        Id = role.Id,
                        RoleName = role.Name
                    };
                    return View(new List<RoleViewModel> { mappedRole });

                }
                return View(Enumerable.Empty<RoleViewModel>());

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel role)
        {


            if (ModelState.IsValid)
            {
               
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(role);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
                
            }

            return View(role);


        }





        public async Task<IActionResult> Details(string id, string viewNAme = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var mappedRole= _mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(mappedRole);
            }
            return NotFound();
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] string id, RoleViewModel roleVm)
        {
            if (roleVm.Id != id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                   role.Name=roleVm.RoleName;
                    role.Id = id;
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(roleVm);
                }
                catch (System.Exception)
                {

                    return BadRequest();
                }
            }
            return View(roleVm);


        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel roleVm)
        {
            if (roleVm != null)
            {
                var role = await _roleManager.FindByIdAsync(roleVm.Id);
                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(roleVm);
                }

            }
            return NotFound();
        }
    }
}
