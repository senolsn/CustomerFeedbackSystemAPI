using Business.Dto.ComplaintRequests;
using Core.Utilities.Results.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IComplaintService
    {
        IResult Add(CreateComplaintRequest createComplaintRequest);
        IResult UpdateForEmployee(UpdateComplaintRequestForEmployee updateComplaintRequestForEmployee);
        IResult UpdateForCustomer(UpdateComplaintRequestForCustomer updateComplaintRequestForCustomer);
        IResult Delete(DeleteComplaintRequest deleteComplaintRequest);
        IDataResult<IEnumerable<Complaint>> GetAllComplaints();
        IDataResult<IEnumerable<Complaint>> GetAllResolvedComplaints();
        IDataResult<IEnumerable<Complaint>> GetAllUnsolvedComplaints();
        IDataResult<Complaint> GetComplaintById(Guid complaintId);

        //Employee
        IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmüş Şikayetleri Listeler.
        IDataResult<IEnumerable<Complaint>> GetCreatedComplaintsByEmployeeId(Guid employeeId);  //Oluşturulmuş şikayetleri getirir.

        //Customer
        IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByCustomerId(Guid customerId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByCustomerId(Guid customerId);   //Çözülmüş Şikayetleri Listeler.
    }
}
