using Business.Dto.Complaint;
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
        IResult Update(Complaint complaint);
        IResult Delete(Complaint complaint);
        IDataResult<IEnumerable<Complaint>> GetAllComplaint();
        IDataResult<Complaint> GetComplaintById(Guid complaintId);

        //Employee
        IDataResult<List<Complaint>> GetUnresolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<List<Complaint>> GetResolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmüş Şikayetleri Listeler.

        //Customer
        IDataResult<List<Complaint>> GetUnresolvedComplaintsByCustomerId(Guid customerId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<List<Complaint>> GetResolvedComplaintsByCustomerId(Guid customerId);   //Çözülmüş Şikayetleri Listeler.
    }
}
