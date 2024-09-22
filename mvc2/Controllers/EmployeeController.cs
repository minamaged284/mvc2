using AutoMapper;
using bll.Interfaces;
using dal.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using mvc2.Helpers;
using mvc2.ViewModels;
using System;
using System.Collections.Generic;


namespace mvc2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork UnitOfWork,IWebHostEnvironment env,IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            this.env = env;
            _mapper = mapper;
        }
        public IActionResult Index(string searchInput)
        {
            if (string.IsNullOrEmpty(searchInput))
            {

                TempData.Keep();
                var employees = _UnitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                return View(mappedEmp);
            }
            else
            {
                var employees = _UnitOfWork.EmployeeRepository.GetEmployeeByName(searchInput);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                return View(mappedEmp);
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {


            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.uploadFile(employee.Image, "Images");
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _UnitOfWork.EmployeeRepository.Add(mappedEmployee);

                int count = _UnitOfWork.complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
          
                return View(employee);
            

        }

        public IActionResult Details(int? id,string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            else
            {
                var employee=_UnitOfWork.EmployeeRepository.GetById(id.Value);
                var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);


                if (mappedEmployee == null)
                {
                    return NotFound();
                }
                return View(viewName, mappedEmployee);
            }
            
        }

        public IActionResult Update(int? id)

        {
            return Details(id,"Update");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id , EmployeeViewModel employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    employee.ImageName = DocumentSettings.uploadFile(employee.Image, "Images");

                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                    _UnitOfWork.EmployeeRepository.Update(mappedEmployee);
                    _UnitOfWork.complete();
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
                        ModelState.AddModelError(string.Empty, "an error occured");
                        
                    }
                    
                }

            }
            return View(employee);

        }




  

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]

        public IActionResult Delete(EmployeeViewModel employee)
        {
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employee);

                _UnitOfWork.EmployeeRepository.Delete(mappedEmployee);
                DocumentSettings.deleteFile(employee.ImageName, "Images");
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
                return View(employee);
            }

        }

    }
}
