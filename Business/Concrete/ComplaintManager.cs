using Business.Abstract;
using Business.Dto.Complaint;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities;
using Entities.enums;
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

        public IResult Delete(DeleteComplaintRequest deleteComplaintRequest)
        {
            var complaintToDelete = _complaintDal.Get(c => c.ComplaintId == deleteComplaintRequest.ComplaintId);
            
            if(complaintToDelete is not null)
            {
                _complaintDal.Delete(complaintToDelete);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Update(UpdateComplaintRequest updateComplaintRequest)
        {
            //var complaintToUpdate = _complaintDal.Get(c => c.ComplaintId == updateComplaintRequest.ComplaintId);

            //Complaint complaint = new Complaint()
            //{
            //    ComplaintId = updateComplaintRequest.ComplaintId,
            //    Description = updateComplaintRequest.Description,
            //    Title = updateComplaintRequest.Title,

            //};
            throw new Exception();

        }

        public IDataResult<IEnumerable<Complaint>> GetAllComplaints()
        {
            var data = _complaintDal.GetAll();

            if(data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }

            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

        public IDataResult<Complaint> GetComplaintById(Guid complaintId)
        {
            var data = _complaintDal.Get(c => c.ComplaintId ==  complaintId);

            if(data is not null)
            {
                return new SuccessDataResult<Complaint>(data);
            }
            return new ErrorDataResult<Complaint>();
        }

        public IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByCustomerId(Guid customerId)
        {
            var data = _complaintDal.GetAll(c => c.Customer.Id == customerId).Where(c => c.ComplaintStatus == ComplaintStatus.RESOLVED);
            
            if( data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }

            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

        public IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByEmployeeId(Guid employeeId)
        {
            var data = _complaintDal.GetAll(c => c.Employee.Id == employeeId).Where(c => c.ComplaintStatus == ComplaintStatus.RESOLVED);

            if( data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }
            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

        public IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByCustomerId(Guid customerId)
        {
            var data = _complaintDal.GetAll(c => c.Customer.Id == customerId).Where(c => c.ComplaintStatus == ComplaintStatus.INPROGRESS);

            if( data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }
            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

        public IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByEmployeeId(Guid employeeId)
        {
            var data = _complaintDal.GetAll(c => c.Employee.Id == employeeId).Where(c => c.ComplaintStatus == ComplaintStatus.INPROGRESS);

            if( data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }
            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

       
    }
}
