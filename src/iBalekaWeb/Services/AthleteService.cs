using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Models;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Data.Infastructure;

namespace iBalekaWeb.Services
{
    public interface IAthleteService
    {
        IEnumerable<Athlete> GetAthletes();
        Athlete GetAthlete(int id);
        void AddAthlete(Athlete athlete);
        void UpdateAthlete(Athlete athlete);
        void DeleteAthlete(Athlete athlete);
        void SaveAthlete();
    }

    public class AthleteService:IAthleteService
    {
        private readonly IAthleteRepository _athleteRepo;
        private readonly IUnitOfWork unitOfWork;

        public AthleteService(IAthleteRepository _repo,IUnitOfWork _unitOfWork)
        {
            _athleteRepo = _repo;
            unitOfWork = _unitOfWork;
        }

        public IEnumerable<Athlete> GetAthletes()
        {
            return _athleteRepo.GetAll();
        }
        public Athlete GetAthlete(int id)
        {
            return _athleteRepo.GetAthleteByID(id);
        }
        public void AddAthlete(Athlete athlete)
        {
            _athleteRepo.Add(athlete);
        }
        public void UpdateAthlete(Athlete athlete)
        {
            _athleteRepo.Update(athlete);
        }
        public void DeleteAthlete(Athlete athlete)
        {
            _athleteRepo.Delete(athlete);
        }
        public void SaveAthlete()
        {
            unitOfWork.Commit();
        }
    }
}
