using Business.Abstract;
using Business.Dto.Auth;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        protected readonly ICustomerService _customerService;
        protected readonly IEmployeeService _employeeService;
        protected readonly ITokenHelper _tokenHelper;

        public AuthManager(ICustomerService customerService, IEmployeeService employeeService, ITokenHelper tokenHelper)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _tokenHelper = tokenHelper;
        }

        public IResult CheckCustomerExists(string email)
        {
            var customer = _customerService.GetByMail(email);
            if (customer is not null) 
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IResult CheckEmployeeExists(string email)
        {
            var employee = _employeeService.GetByMail(email);
            if (employee is not null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateCustomerAccessToken(Customer customer)
        {
            var claims = _customerService.GetClaims(customer);
            var accessToken = _tokenHelper.CreateToken(customer, claims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<AccessToken> CreateEmployeeAccessToken(Employee employee)
        {
            var claims = _employeeService.GetClaims(employee);
            var accessToken = _tokenHelper.CreateToken(employee, claims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<Customer> CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var customerToCheck = _customerService.GetByMail(customerLoginDto.Email);

            if(customerToCheck is null)
            {
                return new ErrorDataResult<Customer>();
            }

            if (!HashingHelper.VerifyPasswordHash(customerLoginDto.Password, customerToCheck.Data.PasswordHash, customerToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<Customer>("Password Error !");
            }

            return new SuccessDataResult<Customer>(customerToCheck.Data);
        }

        public IDataResult<Employee> EmployeeLogin(EmployeeLoginDto employeeRegisterDto)
        {
            var employeeToCheck = _employeeService.GetByMail(employeeRegisterDto.Email);

            if (employeeToCheck is null)
            {
                return new ErrorDataResult<Employee>();
            }

            if (!HashingHelper.VerifyPasswordHash(employeeRegisterDto.Password, employeeToCheck.Data.PasswordHash, employeeToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<Employee>("Password Error !");
            }

            return new SuccessDataResult<Employee>(employeeToCheck.Data);
        }

        public IDataResult<Customer> CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(customerRegisterDto.Password, out passwordHash, out passwordSalt);
            
            var customer = new Customer()
            {
                Email = customerRegisterDto.Email,
                FirstName = customerRegisterDto.FirstName,
                LastName = customerRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _customerService.Add(customer);
            return new SuccessDataResult<Customer>(customer);
        }

        public IDataResult<Employee> EmployeeRegister(EmployeeRegisterDto employeeRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(employeeRegisterDto.Password, out passwordHash, out passwordSalt);
            var employee = new Employee()
            {
                Email = employeeRegisterDto.Email,
                FirstName = employeeRegisterDto.FirstName,
                LastName = employeeRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _employeeService.Add(employee);
            return new SuccessDataResult<Employee>(employee);
        }
    }
}
