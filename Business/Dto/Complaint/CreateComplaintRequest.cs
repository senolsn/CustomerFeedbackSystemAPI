﻿using Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dto.ComplaintRequests
{
    public class CreateComplaintRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
