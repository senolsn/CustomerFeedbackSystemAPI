using Core.DataAccess.EntityFramework.Concrete;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfComplaintDal:EfEntityRepositoryBase<Complaint,CustomerFeedbackSystemAPIContext>,IComplaintDal
    {
        public override List<Complaint> GetAll(Expression<Func<Complaint, bool>> filter = null)
        {
            using (var context = new CustomerFeedbackSystemAPIContext())
            {
                IQueryable<Complaint> query = context.Set<Complaint>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return query.Include(c => c.Employee).Include(c => c.Customer).ToList();
            }
        }

        public override void Update(Complaint entity)
        {
            using (var context = new CustomerFeedbackSystemAPIContext())
            {
                var updatedEntity = context.Entry(entity);

                context.Entry(entity.Employee).State = EntityState.Modified; //Entity Framework'un değişikliği anlaması için modified olarak işaretlenmesi gerekiyor.
                context.Entry(entity.Customer).State = EntityState.Modified; //Entity Framework'un değişikliği anlaması için modified olarak işaretlenmesi gerekiyor.
                updatedEntity.State = EntityState.Modified;

                context.SaveChanges();
            }
        }

    }
}
