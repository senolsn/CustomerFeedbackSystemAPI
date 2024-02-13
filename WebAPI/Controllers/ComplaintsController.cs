using Business.Abstract;
using Business.Dto.ComplaintRequests;
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

        [HttpPost("Add")]
        public IActionResult Add(CreateComplaintRequest createComplaintRequest)
        {
            var result = _complaintService.Add(createComplaintRequest);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteComplaintRequest deleteComplaintRequest) 
        {
            var result = _complaintService.Delete(deleteComplaintRequest);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateForCustomer")]
        public IActionResult UpdateForCustomer(UpdateComplaintRequestForCustomer updateComplaintRequestForCustomer) 
        {
            var result = _complaintService.UpdateForCustomer(updateComplaintRequestForCustomer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateForEmployee")]
        public IActionResult UpdateForEmployee(UpdateComplaintRequestForEmployee updateComplaintRequestForEmployee)
        {
            var result = _complaintService.UpdateForEmployee(updateComplaintRequestForEmployee);
            if(result.Success)
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

        [HttpGet("GetComplaintById")]
        public IActionResult GetComplaintById(Guid complaintId) 
        {
            var result = _complaintService.GetComplaintById(complaintId);
            if(result.Success )
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

        [HttpGet("GetUnresolvedComplaintsByCustomerId")]
        public IActionResult GetUnresolvedComplaintsByCustomerId(Guid customerId)
        {
            var result = _complaintService.GetUnresolvedComplaintsByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetUnresolvedComplaintsByEmployeeId")]
        public IActionResult GetUnresolvedComplaintsByEmployeeId(Guid employeeId)
        {
            var result = _complaintService.GetUnresolvedComplaintsByEmployeeId(employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
