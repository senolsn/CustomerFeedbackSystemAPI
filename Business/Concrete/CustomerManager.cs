using Business.Abstract;
using Business.Dto.Customer;
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
    public class CustomerManager : ICustomerService
    {
        protected readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public void Add(Customer customer)
        {
           _customerDal.Add(customer);
        }

        public IDataResult<Customer> GetById(Guid customerId)
        {
            var data = _customerDal.Get(c => c.Id == customerId);

            if(data is not null)
            {
                return new SuccessDataResult<Customer>(data);
            }
            return new ErrorDataResult<Customer>();
        }

        public IDataResult<Customer> GetByMail(string email)
        {
            var data = _customerDal.Get(c => c.Email == email);
            if(data is not null)
            {
                return new SuccessDataResult<Customer>(data);
            }
            return new ErrorDataResult<Customer>();
        }

        public List<OperationClaim> GetClaims(Customer customer)
        {
            return _customerDal.GetClaims(customer);
        }

        public IResult UserForUpdate(CustomerUpdateDto customerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
