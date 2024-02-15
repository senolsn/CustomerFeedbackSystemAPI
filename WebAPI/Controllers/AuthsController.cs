using Business.Abstract;
using Business.Dto.Auth;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthsController : Controller
    {
        protected readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterForEmployee")]
        public IActionResult RegisterForEmployee(EmployeeRegisterDto employeeRegisterDto)
        {
            var employeeExists = _authService.CheckEmployeeExists(employeeRegisterDto.Email);

            if(employeeExists.Success)
            {
                return BadRequest(employeeExists.Message);
            }

            var registerForEmployee = _authService.EmployeeRegister(employeeRegisterDto);

            var result = _authService.CreateEmployeeAccessToken(registerForEmployee.Data);

            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("RegisterForCustomer")]
        public IActionResult RegisterForCustomer(CustomerRegisterDto customerRegisterDto)
        {
            var customerExists = _authService.CheckCustomerExists(customerRegisterDto.Email);

            if(customerExists.Success)
            {
                return BadRequest(customerExists.Message);
            }

            var registerForCustomer = _authService.CustomerRegister(customerRegisterDto);
            var result = _authService.CreateCustomerAccessToken(registerForCustomer.Data);

            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("LoginForEmployee")]
        public ActionResult LoginForEmployee(EmployeeLoginDto employeeLoginDto)
        {
            var userToLogin = _authService.EmployeeLogin(employeeLoginDto);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateEmployeeAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("LoginForCustomer")]
        public ActionResult LoginForCustomer(CustomerLoginDto customerLoginDto)
        {
            var userToLogin = _authService.CustomerLogin(customerLoginDto);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateCustomerAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
