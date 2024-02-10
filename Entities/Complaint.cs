﻿using Core.Entities.Abstract;
using Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Complaint : IEntity
    {
        public Guid ComplaintId { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ComplaintStatus ComplaintStatus { get; set; } = ComplaintStatus.INPROGRESS;
        public string? EmployeeNote { get; set; }
        public int Score { get; set; }
    }
}
