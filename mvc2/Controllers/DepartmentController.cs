using Microsoft.AspNetCore.Mvc;
using bll.Interfaces;
using dal.Data;
using dal.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Hosting;


namespace mvc2.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository repo;
        private readonly IWebHostEnvironment env;

        public DepartmentController(IDepartmentRepository repo,IWebHostEnvironment env)
        {
            this.repo = repo;
            this.env = env;
        }
        public IActionResult Index()
        {
            var department = repo.GetAll();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                int count = repo.Add(department);
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
                if (department == null)
                {
                    return NotFound();
                }

                else
                {
                    return View(department);
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
        public IActionResult Update([FromRoute]int id,Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid) {

                try
                {
                    repo.Update(department);
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
        public IActionResult Delete(Department department)
        {
            try
            {
                repo.Delete(department);
                    
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
