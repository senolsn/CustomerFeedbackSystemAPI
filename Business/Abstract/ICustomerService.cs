using Business.Dto.Customer;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        List<OperationClaim> GetClaims(Customer customer);
        void Add(Customer customer);
        IDataResult<Customer> GetByMail(string email);
        IResult UserForUpdate(CustomerUpdateDto customerUpdateDto);
        IDataResult<Customer> GetById(Guid customerId);
    }
}
