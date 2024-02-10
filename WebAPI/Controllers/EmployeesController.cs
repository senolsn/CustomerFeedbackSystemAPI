using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
