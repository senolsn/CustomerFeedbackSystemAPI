using Business.Dto.Rating;
using Core.Utilities.Results.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRatingService
    {
        IResult Add(CreateRatingRequest createRatingRequest);
        IResult Update(UpdateRatingRequest updateRatingRequest);
        IResult UpdateRatingByComplaintId(UpdateRatingRequest updateRatingRequest);
        IResult Delete(DeleteRatingRequest deleteRatingRequest);
        IDataResult<Rating> GetById(Guid ratingId);
        IDataResult<IEnumerable<Rating>> GetAllRatings();
        IDataResult<IEnumerable<Rating>> GetAllRatingsByEmployeeId(Guid employeeId);
        IDataResult<double> CalculateRatingByEmployeeId(Guid employeeId);
        IDataResult<IDictionary<Guid, double>> CalculateRatingsForAllEmployees();

    }
}
