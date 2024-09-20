using Microsoft.AspNetCore.Mvc;
using bll.Interfaces;
using dal.Data;
using dal.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System.Collections;
using mvc2.ViewModels;
using System.Collections.Generic;


namespace mvc2.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository repo;
        private readonly IWebHostEnvironment env;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository repo,IWebHostEnvironment env,IMapper mapper)
        {
            this.repo = repo;
            this.env = env;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var department = repo.GetAll();
            var mappedDepartment = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);


            return View(mappedDepartment);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment=_mapper.Map<DepartmentViewModel,Department>(department);
                int count = repo.Add(mappedDepartment);
                if (count > 0)
                {

                    
                   return RedirectToAction(nameof(Index));
                }

              

            }
            return View(department);

        }

        public IActionResult Details(int? id,string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            else
            {
                var department = repo.GetById(id.Value);
                var mappedDepartment = _mapper.Map<Department, DepartmentViewModel>(department);

                if (mappedDepartment == null)
                {
                    return NotFound();
                }

                else
                {
                    return View(mappedDepartment);
                }
            }
            


        }

        public IActionResult Update(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}

            //else
            //{
            //    var department = repo.GetById(id.Value);
            //    if (department == null)
            //    {
            //        return NotFound();

            //    }

            //    else
            //    {
            //        return View(department);
            //    }
            //}
            return Details(id,"Update");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int id,DepartmentViewModel department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid) {

                try
                {
                    var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(department);

                    repo.Update(mappedDepartment);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    if (env.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty,ex.Message);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "an error occured ");
                    }
                    return View(department);
                }
                
            }

            else { return View(department); }


           
        }

        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel department)
        {
            try
            {
                var mappedDepartment = _mapper.Map<DepartmentViewModel, Department>(department);

                repo.Delete(mappedDepartment);
                    
                return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {

                if (env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "an error occured ");
                }
                return View(department);
            }

        }

    }
}
