using bll.Interfaces;
using dal.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace mvc2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repo;
        private readonly IWebHostEnvironment env;

        public EmployeeController(IEmployeeRepository Repo,IWebHostEnvironment env)
        {
            repo = Repo;
            this.env = env;
        }
        public IActionResult Index()
        {
            TempData.Keep();
            var employees = repo.GetAll();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                int count = repo.Add(employee);
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
                var employee=repo.GetById(id.Value);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(viewName,employee);
            }
            
        }

        public IActionResult Update(int? id)

        {
            return Details(id,"Update");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int id , Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                     repo.Update(employee);
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

        public IActionResult Delete(Employee employee)
        {
            try
            {
                repo.Delete(employee);

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
