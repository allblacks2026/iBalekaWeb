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
    public interface IClubMemberService
    {
        ClubMember GetMemberByID(int id);
        IEnumerable<ClubMember> GetAll();
        void AddMember(ClubMember member);
        void UpdateMember(ClubMember member);
        void Delete(ClubMember member);
        void SaveMember();

    }
    public class ClubMemberService:IClubMemberService
    {
        private readonly IClubMemberRepository _routeRepo;
        private readonly IUnitOfWork unitOfWork;

        public ClubMemberService(IClubMemberRepository _repo,IUnitOfWork _unitOfWork)
        {
            _routeRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        
        public ClubMember GetMemberByID(int id)
        {
            return _routeRepo.GetMemberByID(id);
        }
        public IEnumerable<ClubMember> GetAll()
        {
            return _routeRepo.GetAll();
        }
        public void AddMember(ClubMember member)
        {
            _routeRepo.Add(member);
        }
        public void UpdateMember(ClubMember member)
        {
            _routeRepo.Update(member);
        }
        public void Delete(ClubMember member)
        {
            _routeRepo.Delete(member);
        }
        public void SaveMember()
        {
            unitOfWork.Commit();
        }
    }
}
