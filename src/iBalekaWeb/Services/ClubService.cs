using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Models;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Data.Repositories;

namespace iBalekaWeb.Services
{
    public interface IClubService
    {
        Club GetClubByID(int id);
        IEnumerable<Club> GetAll();
        void AddClub(Club club);
        void UpdateClub(Club club);
        void Delete(Club club);
        void SaveClub();
    }
    public class ClubService:IClubService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IUnitOfWork unitOfWork;

        public ClubService(IClubRepository _repo,IUnitOfWork _unitOfWork)
        {
            _clubRepo = _repo;
            unitOfWork = _unitOfWork;
        }

        public Club GetClubByID(int id)
        {
            return _clubRepo.GetClubByID(id);
        }
        public IEnumerable<Club> GetAll()
        {
            return _clubRepo.GetAll();
        }
        public void AddClub(Club club)
        {
            _clubRepo.Add(club);
        }
        public void UpdateClub(Club club)
        {
            _clubRepo.Update(club);
        }
        public void Delete(Club club)
        {
            _clubRepo.Delete(club);
        }
        public void SaveClub()
        {
            unitOfWork.Commit();
        }
    }
}
