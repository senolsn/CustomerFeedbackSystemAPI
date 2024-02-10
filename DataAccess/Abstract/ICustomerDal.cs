using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Customer>
    {
        List<OperationClaim> GetClaims(Customer customer);
    }
}
