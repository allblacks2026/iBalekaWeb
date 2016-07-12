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
    //public interface IUserService
    //{
    //    User GetUserByID(int id);
    //    IEnumerable<User> GetAll();
    //    void AddUser(User user);
    //    void UpdateUser(User user);
    //    void Delete(User user);
    //    void SaveUser();
    //}
    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepo;
    //    private readonly IUnitOfWork unitOfWork;

    //    public UserService(IUserRepository _repo, IUnitOfWork _unitOfWork)
    //    {
    //        _userRepo = _repo;
    //        unitOfWork = _unitOfWork;
    //    }

    //    public User GetUserByID(int id)
    //    {
    //        return _userRepo.GetUserByID(id);
    //    }
    //    public IEnumerable<User> GetAll()
    //    {
    //        return _userRepo.GetAll();
    //    }
    //    public void AddUser(User user)
    //    {
    //        _userRepo.Add(user);
    //    }
    //    public void UpdateUser(User user)
    //    {
    //        _userRepo.Update(user);
    //    }
    //    public void Delete(User user)
    //    {
    //        _userRepo.Delete(user);
    //    }
    //    public void SaveUser()
    //    {
    //        unitOfWork.Commit();
    //    }
    //}
}
