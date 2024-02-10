using Business.Abstract;
using Business.Dto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class CustomersController : Controller
    {
        protected readonly ICustomerService _customerService;

       
      
    }
}
