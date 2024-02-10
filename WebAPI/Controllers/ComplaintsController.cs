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
        
    }
}
