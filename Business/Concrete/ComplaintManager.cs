using Business.Abstract;
using Business.Dto.ComplaintRequests;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities;
using Entities.enums;


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
            var rules = BusinessRules.Run(CanAssignMoreComplaintsToEmployee(createComplaintRequest),CanAssignMoreCreatedComplaintsToEmployee(createComplaintRequest));

            if(rules is not null)
            {
                return rules;
            }

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

        public IResult UpdateForCustomer(UpdateComplaintRequestForCustomer updateComplaintRequestForCustomer)
        {
            var rules = BusinessRules.Run(CanCustomerUpdateComplaintForCustomer(updateComplaintRequestForCustomer), CanAssignMoreComplaintsToEmployee(updateComplaintRequestForCustomer));
            
            if(rules is not null)
            {
                return rules;
            }

            var updateToComplaint = _complaintDal.Get(c => c.ComplaintId == updateComplaintRequestForCustomer.ComplaintId);

            if(updateToComplaint is null)
            {
                return new ErrorResult("Hata! Böyle bir şikayet bulunamadı.");
            }

            var employee = _employeeService.GetById(updateComplaintRequestForCustomer.EmployeeId);
            var customer = _customerService.GetById(updateComplaintRequestForCustomer.CustomerId);

            updateToComplaint.ComplaintId = updateComplaintRequestForCustomer.ComplaintId;
            updateToComplaint.Title = updateComplaintRequestForCustomer.Title;
            updateToComplaint.Description = updateComplaintRequestForCustomer.Description;
            updateToComplaint.ComplaintStatus = updateComplaintRequestForCustomer.ComplaintStatus;
            updateToComplaint.Employee = employee.Data;
            updateToComplaint.Customer = customer.Data;

            _complaintDal.Update(updateToComplaint);

            return new SuccessResult(); 
        }

        public IResult UpdateForEmployee(UpdateComplaintRequestForEmployee updateComplaintRequestForEmployee)
        {
            var rules = BusinessRules.Run(CanChangeComplaintStatusForEmployee(updateComplaintRequestForEmployee));

            if(rules is not null)
            {
                return rules;
            }

            var updateToComplaint = _complaintDal.Get(c => c.ComplaintId == updateComplaintRequestForEmployee.ComplaintId);

            if(updateToComplaint is null)
            {
                return new ErrorResult();
            }

            var employee = _employeeService.GetById(updateComplaintRequestForEmployee.EmployeeId);
            var customer = _customerService.GetById(updateComplaintRequestForEmployee.CustomerId);

            updateToComplaint.ComplaintId = updateComplaintRequestForEmployee.ComplaintId;
            updateToComplaint.Title = updateComplaintRequestForEmployee.Title;
            updateToComplaint.Description = updateComplaintRequestForEmployee.Description;
            updateToComplaint.ComplaintStatus = updateComplaintRequestForEmployee.ComplaintStatus;
            updateToComplaint.EmployeeNote = updateComplaintRequestForEmployee.EmployeeNote;
            updateToComplaint.Employee = employee.Data;
            updateToComplaint.Customer = customer.Data;

            _complaintDal.Update(updateToComplaint);
            return new SuccessResult();
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

        public IDataResult<IEnumerable<Complaint>> GetAllResolvedComplaints()
        {
            var data = _complaintDal.GetAll(c => c.ComplaintStatus == ComplaintStatus.RESOLVED);
            if(data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }
            return new ErrorDataResult<IEnumerable<Complaint>>();
        }

        public IDataResult<IEnumerable<Complaint>> GetAllUnsolvedComplaints()
        {
            var data = _complaintDal.GetAll(c => c.ComplaintStatus == ComplaintStatus.INPROGRESS || c.ComplaintStatus == ComplaintStatus.CREATED);
            if (data is not null)
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

        public IDataResult<IEnumerable<Complaint>> GetCreatedComplaintsByEmployeeId(Guid employeeId)
        {
            var data = _complaintDal.GetAll(c => c.Employee.Id == employeeId).Where(c => c.ComplaintStatus == ComplaintStatus.CREATED);
            if(data is not null)
            {
                return new SuccessDataResult<IEnumerable<Complaint>>(data);
            }
            return new ErrorDataResult<IEnumerable<Complaint>>();
        }



        //HELPER METHODS
        //1- EĞER ComplaintStatus == ComplaintStatus.INPROGRESS Customer herhangi bir update işlemi gerçekleştiremez.
        //2- EĞER Bir Employee'de 5'den fazla ComplaintStatus.INPROGRESS Şikayet var ise o employee'ye daha fazla iş atanamaz.
        //3- Employee ComplaintStatus'ü değiştirmek için (Datetime.Now - CreatedDate) > 3 koşulunu sağlamalıdır.

        private IResult CanCustomerUpdateComplaintForCustomer(UpdateComplaintRequestForCustomer updateComplaintRequestForCustomer) 
        {
            var result = updateComplaintRequestForCustomer.ComplaintStatus != ComplaintStatus.INPROGRESS;

            if(result)
            {
                return new SuccessResult();
            }

            return new ErrorResult("Hata! Güncellemeye çalıştığınız şikayet şu an işlem aşamasındadır. Lütfen yetkili birimlere ulaşınız.");
        }
        private IResult CanAssignMoreComplaintsToEmployee(CreateComplaintRequest createComplaintRequest)
        {
            var unresolvedComplaints = GetUnresolvedComplaintsByEmployeeId(createComplaintRequest.EmployeeId);

            if(unresolvedComplaints.Data.Count() > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CanAssignMoreCreatedComplaintsToEmployee(CreateComplaintRequest createComplaintRequest)
        {
            var createdComplaints = GetCreatedComplaintsByEmployeeId(createComplaintRequest.EmployeeId);

            if (createdComplaints.Data.Count() > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CanAssignMoreComplaintsToEmployee(UpdateComplaintRequestForCustomer updateComplaintRequestForCustomer)
        {
            var unresolvedComplaints = GetUnresolvedComplaintsByEmployeeId(updateComplaintRequestForCustomer.EmployeeId);

            if (unresolvedComplaints.Data.Count() > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult CanChangeComplaintStatusForEmployee(UpdateComplaintRequestForEmployee updateComplaintRequestForEmployee)
        {
            var complaint = _complaintDal.Get(c => c.ComplaintId == updateComplaintRequestForEmployee.ComplaintId);
            var result = (DateTime.Now - complaint.CreatedDate).TotalDays;

            if( result > 3 || complaint.ComplaintStatus == ComplaintStatus.CREATED)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Müşteri şikayeti kapatmadığından dolayı minimum 3 günden önce şikayet kapatılamaz!");
        }

    }
}
