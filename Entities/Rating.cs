using Entities.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Rating
    {
        public Guid RatingId { get; set; }
        public RatingStatus RatingStatus { get; set; }
        public Guid ComplaintId { get; set; }
        public Complaint Complaint { get; set; }
    }
}
