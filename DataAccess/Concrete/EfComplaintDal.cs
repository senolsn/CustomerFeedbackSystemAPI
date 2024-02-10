using Core.DataAccess.EntityFramework.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfComplaintDal:EfEntityRepositoryBase<Complaint,CustomerFeedbackSystemAPIContext>,IComplaintDal
    {
    }
}
