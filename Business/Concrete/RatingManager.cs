using Business.Abstract;
using Business.Dto.Rating;
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
    public class RatingManager : IRatingService
    {
        protected readonly IRatingDal _ratingDal;
        protected readonly IComplaintService _complaintService;
        protected readonly IEmployeeService _employeeService;

        public RatingManager(IRatingDal ratingDal, IComplaintService complaintService, IEmployeeService employeeService)
        {
            _ratingDal = ratingDal;
            _complaintService = complaintService;
            _employeeService = employeeService;
        }

        public IResult Add(CreateRatingRequest createRatingRequest)
        {

            var rating = new Rating()
            {
                ComplaintId = createRatingRequest.ComplaintId,
                RatingStatus = createRatingRequest.RatingStatus,
            };

            var complaint = _complaintService.GetComplaintById(createRatingRequest.ComplaintId);
            rating.Complaint = complaint.Data;

            if(rating is not null)
            {
                _ratingDal.Add(rating);
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        public IResult Delete(DeleteRatingRequest deleteRatingRequest)
        {
            var ratingToDelete = _ratingDal.Get(r => r.RatingId == deleteRatingRequest.RatingId);

            if(ratingToDelete is not null)
            {
                _ratingDal.Delete(ratingToDelete);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Update(UpdateRatingRequest updateRatingRequest)
        {
            var updateToRating = _ratingDal.Get(r => r.RatingId == updateRatingRequest.RatingId);

            if (updateToRating is null)
            {
                return new ErrorResult();
            }

            updateToRating.RatingId = updateRatingRequest.RatingId;
            updateToRating.RatingStatus = updateRatingRequest.RatingStatus;
            updateToRating.ComplaintId = updateRatingRequest.ComplaintId;
            
            _ratingDal.Update(updateToRating);

            return new SuccessResult();
        }
        public IResult UpdateRatingByComplaintId(UpdateRatingRequest updateRatingRequest)
        {
            var updateToRating = _ratingDal.Get(r => r.ComplaintId == updateRatingRequest.ComplaintId);

            if(updateToRating is null)
            {
                return new ErrorResult();
            }
            updateToRating.ComplaintId = updateRatingRequest.ComplaintId;
            updateToRating.RatingStatus = updateRatingRequest.RatingStatus;

            _ratingDal.Update(updateToRating);
            return new SuccessResult();
        }

        public IDataResult<IEnumerable<Rating>> GetAllRatings()
        {
            var data = _ratingDal.GetAll();

            if(data is null)
            {
                return new ErrorDataResult<IEnumerable<Rating>>();
            }

            return new SuccessDataResult<IEnumerable<Rating>>(data);
        }

        public IDataResult<IEnumerable<Rating>> GetAllRatingsByEmployeeId(Guid employeeId)
        {
            var data = _ratingDal.GetAll(r => r.Complaint.Employee.Id == employeeId);

            if (data is null)
            {
                return new ErrorDataResult<IEnumerable<Rating>>();
            }
            return new SuccessDataResult<IEnumerable<Rating>>(data);
        }

        public IDataResult<Rating> GetById(Guid ratingId)
        {
            var data = _ratingDal.Get(r => r.RatingId  == ratingId);
            if ( data is null)
            {
                return new ErrorDataResult<Rating>();
            }
            return new SuccessDataResult<Rating>(data);
        }

        public IDataResult<double> CalculateRatingByEmployeeId(Guid employeeId)
        {

            var ratings = GetAllRatingsByEmployeeId(employeeId).Data;

            if (ratings is null || !ratings.Any())
            {
                return new SuccessDataResult<double>(0);
            }

            var averageRating = ratings.Average(r => (double)r.RatingStatus);

            return new SuccessDataResult<double>(averageRating);
        }

        public IDataResult<IDictionary<Guid,double>> CalculateRatingsForAllEmployees()
        {
            var allEmployees = _employeeService.GetAll();
            var ratingsForAllEmployees = new Dictionary<Guid, double>();

            foreach (var employee in allEmployees.Data)
            {
                var rating = CalculateRatingByEmployeeId(employee.Id);
                ratingsForAllEmployees.Add(employee.Id, rating.Data);
            }

            return new SuccessDataResult<IDictionary<Guid,double>>(ratingsForAllEmployees);
        }
     }
}
