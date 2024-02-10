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
    public interface IEmployeeDal:IEntityRepository<Employee>
    {
        List<OperationClaim> GetClaims(Employee employee);
    }
}
