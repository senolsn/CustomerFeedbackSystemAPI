using Business.Dto.Customer;
using Business.Dto.Employee;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        List<OperationClaim> GetClaims(Employee employee);
        void Add(Employee employee);
        IDataResult<Employee> GetByMail(string email);
        IResult UserForUpdate(EmployeeUpdateDto employeeUpdateDto);

        IDataResult<IEnumerable<Employee>> GetAll();

        IDataResult<Employee> GetById(Guid employeeId);

    }
}
