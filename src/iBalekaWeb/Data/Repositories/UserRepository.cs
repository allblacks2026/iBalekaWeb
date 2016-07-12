using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iBalekaWeb.Models;
using iBalekaWeb.Data.Infastructure;

namespace iBalekaWeb.Data.Repositories
{
    //public interface IUserRepository : IRepository<User>
    //{
    //    User GetUserByID(int id);
    //}
    //public class UserRepository : RepositoryBase<User>, IUserRepository
    //{
    //    public UserRepository(IDbFactory dbFactory)
    //        : base(dbFactory) { }
    //    public User GetUserByID(int id)
    //    {
    //        return DbContext.User.Where(a => a.UserId == id && a.Deleted == false).SingleOrDefault();
    //    }
    //    public override IEnumerable<User> GetAll()
    //    {
    //        return DbContext.User.Where(a => a.Deleted == false).ToList();
    //    }
    //    public override void Delete(User user)
    //    {
    //        User deletedUser = GetUserByID(user.UserId);
    //        if (deletedUser != null)
    //        {
    //            deletedUser.Deleted = true;
    //            DbContext.Entry(deletedUser).State = EntityState.Modified;
    //        }
    //    }

    //}
}
