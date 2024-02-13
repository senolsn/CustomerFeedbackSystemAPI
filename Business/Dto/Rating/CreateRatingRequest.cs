using Entities;
using Entities.enums;

namespace Business.Dto.Rating
{
    public class CreateRatingRequest
    {
        public Guid ComplaintId { get; set; }
        public RatingStatus RatingStatus { get; set; }
    }
}