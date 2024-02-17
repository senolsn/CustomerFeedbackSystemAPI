using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        protected readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetByMail")]
        public IActionResult GetByMail(string mail)
        {
            var result = _employeeService.GetByMail(mail);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();


        }
    }
}
