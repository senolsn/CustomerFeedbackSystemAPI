using Business.Abstract;
using Business.Dto.Complaint;
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
    public class ComplaintManager : IComplaintService
    {

        protected readonly IComplaintDal _complaintDal;
        protected readonly ICustomerService _customerService;
        protected readonly IEmployeeService _employeeService;

        public ComplaintManager(IComplaintDal complaintDal, ICustomerService customerService, IEmployeeService employeeService)
        {
            _complaintDal = complaintDal;
            _customerService = customerService;
            _employeeService = employeeService;
        }

        public IResult Add(CreateComplaintRequest createComplaintRequest)
        {
            Complaint complaint = new Complaint()
            {
                EmployeeNote = createComplaintRequest.EmployeeNote,
                Score = createComplaintRequest.Score,
                Description = createComplaintRequest.Description,
                Title = createComplaintRequest.Title,
                CreatedDate = DateTime.UtcNow,
            };

            var customer = _customerService.GetById(createComplaintRequest.CustomerId);
            complaint.Customer = customer.Data;

            var employee = _employeeService.GetById(createComplaintRequest.EmployeeId);
            complaint.Employee = employee.Data;
            
            if(complaint is not null)
            {
                _complaintDal.Add(complaint);
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        public IResult Delete(Complaint complaint)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Complaint>> GetAllComplaint()
        {
            throw new NotImplementedException();
        }

        public IDataResult<Complaint> GetComplaintById(Guid complaintId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Complaint>> GetResolvedComplaintsByCustomerId(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Complaint>> GetResolvedComplaintsByEmployeeId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Complaint>> GetUnresolvedComplaintsByCustomerId(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Complaint>> GetUnresolvedComplaintsByEmployeeId(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Complaint complaint)
        {
            throw new NotImplementedException();
        }
    }
}
