using Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dto.ComplaintRequests
{
    public class UpdateComplaintRequestForEmployee
    {
        public Guid ComplaintId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; }
        public string? EmployeeNote { get; set; }
    }
}
