using Business.Abstract;
using Business.Dto.Employee;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        protected readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public void Add(Employee employee)
        {
            _employeeDal.Add(employee);
        }

        public IDataResult<IEnumerable<Employee>> GetAll()
        {
            var data = _employeeDal.GetAll();

            if(data is null)
            {
                return new ErrorDataResult<IEnumerable<Employee>>();
            }

            return new SuccessDataResult<IEnumerable<Employee>>(data);  
        }

        public IDataResult<Employee> GetById(Guid employeeId)
        {
            var data = _employeeDal.Get(e => e.Id == employeeId);

            if(data is not null)
            {
                return new SuccessDataResult<Employee>(data);
            }
            return new ErrorDataResult<Employee>();
        }

        public IDataResult<Employee> GetByMail(string email)
        {
            var data = _employeeDal.Get(e => e.Email == email);
            if( data is not null )
            {
                return new SuccessDataResult<Employee>(data);
            }

            return new ErrorDataResult<Employee>();
        }

        public List<OperationClaim> GetClaims(Employee employee)
        {
            return _employeeDal.GetClaims(employee);
        }

        public IResult UserForUpdate(EmployeeUpdateDto employeeUpdateDto)
        {
            throw new NotImplementedException();
        }


    }
}
