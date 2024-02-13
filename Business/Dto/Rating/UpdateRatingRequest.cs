using Entities.enums;

namespace Business.Dto.Rating
{
    public class UpdateRatingRequest
    {
        public Guid RatingId { get; set; }
        public RatingStatus RatingStatus { get; set; }
        public Guid ComplaintId { get; set; }
    }
}