using Business.Dto.Auth;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<Employee> EmployeeRegister(EmployeeRegisterDto employeeRegisterDto);
        IDataResult<Customer> CustomerRegister(CustomerRegisterDto customerRegisterDto);
        IDataResult<Employee> EmployeeLogin(EmployeeLoginDto employeeRegisterDto);
        IDataResult<Customer> CustomerLogin(CustomerLoginDto customerLoginDto);
        IDataResult<AccessToken> CreateCustomerAccessToken(Customer customer);
        IDataResult<AccessToken> CreateEmployeeAccessToken(Employee employee);
        IResult CheckEmployeeExists(string email);
        IResult CheckCustomerExists(string email);
    }
}
