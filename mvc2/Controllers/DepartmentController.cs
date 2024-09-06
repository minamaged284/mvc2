using Microsoft.AspNetCore.Mvc;
using bll.Reposatories;
using bll.Interfaces;


namespace mvc2.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentReposatory repo;

        public DepartmentController(IDepartmentReposatory repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            repo.GetAll();  
            return View();
        }
    }
}
