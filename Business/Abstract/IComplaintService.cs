﻿using Business.Dto.Complaint;
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
        IResult Update(UpdateComplaintRequest updateComplaintRequest);
        IResult Delete(DeleteComplaintRequest deleteComplaintRequest);
        IDataResult<IEnumerable<Complaint>> GetAllComplaints();
        IDataResult<Complaint> GetComplaintById(Guid complaintId);

        //Employee
        IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByEmployeeId(Guid employeeId); //Çözülmüş Şikayetleri Listeler.

        //Customer
        IDataResult<IEnumerable<Complaint>> GetUnresolvedComplaintsByCustomerId(Guid customerId); //Çözülmemiş Şikayetleri Listeler.
        IDataResult<IEnumerable<Complaint>> GetResolvedComplaintsByCustomerId(Guid customerId);   //Çözülmüş Şikayetleri Listeler.
    }
}
