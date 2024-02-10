using Core.Entities.Abstract;
using Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Rating : IEntity
    {
        public Guid RatingId { get; set; }
        public Score Score { get; set; }
        public Employee Employee { get; set; }

    }
    
}
