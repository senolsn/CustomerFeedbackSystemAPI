using Business.Abstract;
using Business.Dto.Rating;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RatingsController : Controller
    {
        protected readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("Add")]
        public IActionResult Add(CreateRatingRequest createRatingRequest)
        {
            var result = _ratingService.Add(createRatingRequest);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteRatingRequest deleteRatingRequest)
        {
            var result = _ratingService.Delete(deleteRatingRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public IActionResult Update(UpdateRatingRequest updateRatingRequest)
        {
            var result = _ratingService.Update(updateRatingRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateRatingByComplaintId")]
        public IActionResult UpdateRatingByComplaintId(UpdateRatingRequest updateRatingRequest)
        {
            var result = _ratingService.UpdateRatingByComplaintId(updateRatingRequest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllRatings")]
        public IActionResult GetAllRatings()
        {
            var result = _ratingService.GetAllRatings();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllRatingsByEmployeeId")]
        public IActionResult GetAllRatingsByEmployeeId(Guid employeeId)
        {
            var result = _ratingService.GetAllRatingsByEmployeeId(employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid ratingId)
        {
            var result = _ratingService.GetById(ratingId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("CalculateRatingByEmployeeId")]
        public IActionResult CalculateRatingByEmployeeId(Guid employeeId) 
        {
            var result = _ratingService.CalculateRatingByEmployeeId(employeeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("CalculateRatingsForAllEmployees")]
        public IActionResult CalculateRatingsForAllEmployees()
        {
            var result = _ratingService.CalculateRatingsForAllEmployees();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
