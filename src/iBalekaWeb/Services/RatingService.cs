using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaWeb.Services
{
    public interface IRatingService
    {
        IEnumerable<Rating> GetAll();
        Rating GetByRun(int id);
        Rating GetRatingByID(int id);
        void AddRating(Rating rating);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
        void SaveRating();
    }
    public class RatingService:IRatingService
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IRatingRepository _Repo, IUnitOfWork unitOfWork)
        {
            _ratingRepo = _Repo;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Rating> GetAll()
        {
            return _ratingRepo.GetAll();
        }
        public Rating GetByRun(int id)
        {
            return _ratingRepo.GetByRun(id);
        }
        public Rating GetRatingByID(int id)
        {
            return _ratingRepo.GetRatingByID(id);
        }
        public void AddRating(Rating rating)
        {
            _ratingRepo.Add(rating);
        }
        public void UpdateRating(Rating rating)
        {
            _ratingRepo.Update(rating);
        }
        public void DeleteRating(Rating rating)
        {
            _ratingRepo.Delete(rating);
        }
        public void SaveRating()
        {
            _unitOfWork.Commit();
        }
    }
}
