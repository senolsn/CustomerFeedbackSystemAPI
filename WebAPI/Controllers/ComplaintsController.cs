using Business.Abstract;
using Business.Dto.Complaint;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ComplaintsController : Controller
    {
        protected readonly IComplaintService _complaintService;

        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost]
        public ActionResult Add(CreateComplaintRequest createComplaintRequest)
        {
            var result = _complaintService.Add(createComplaintRequest);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetResolvedComplaintsByCustomerId")]
        public IActionResult GetResolvedComplaintsByCustomerId(Guid customerId)
        {
            var result = _complaintService.GetResolvedComplaintsByCustomerId(customerId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetResolvedComplaintsByEmployeeId")]
        public IActionResult GetResolvedComplaintsByEmployeeId(Guid employeeId)
        {
            var result = _complaintService.GetResolvedComplaintsByEmployeeId(employeeId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllComplaints")]
        public IActionResult GetAllComplaints()
        {
            var result = _complaintService.GetAllComplaints();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
