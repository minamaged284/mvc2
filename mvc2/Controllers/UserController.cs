using AutoMapper;
using dal.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FName = u.FName,
                    LName = u.LName,
                    PhoneNumber = u.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToListAsync();
                return View(users);
            }

            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var mappedUser = new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FName = user.FName,
                        LName = user.LName,
                        PhoneNumber = user.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(user).Result
                    };
                    return View(new List<UserViewModel> { mappedUser });

                }
                return View(Enumerable.Empty<UserViewModel>());

            }
        }

        public async Task<IActionResult> Details(string id, string viewNAme = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
                return View(mappedUser);
            }
            return NotFound();
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] string id, UserViewModel userVm)
        {
            if (userVm.Id != id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var user =await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = userVm.PhoneNumber;
                    user.Email = userVm.Email;
                    user.LName = userVm.LName;
                    user.FName = userVm.FName;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(userVm);
                }
                catch (System.Exception)
                {

                    return BadRequest();
                }
            }
            return View(userVm);


        }

        public async Task<IActionResult> Delete(string id)
        {
           return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel userVm)
        {
            if (userVm != null)
            {
                var user = await _userManager.FindByIdAsync(userVm.Id);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(userVm);
                }

            }
            return NotFound();
        }
    }
}
