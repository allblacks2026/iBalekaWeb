using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;

namespace iBalekaWeb.Data.Repositories
{
    public interface IClubMemberRepository:IRepository<ClubMember>
    {
        ClubMember GetMemberByID(int id);
    }
    public class ClubMemberRepository:RepositoryBase<ClubMember>,IClubMemberRepository
    {
        public ClubMemberRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public ClubMember GetMemberByID(int id)
        {
            return DbContext.ClubMember.Where(m => m.MemberId == id && m.IsaMember == true).SingleOrDefault();
        }
        public override IEnumerable<ClubMember> GetAll()
        {
            return DbContext.ClubMember.Where(a => a.IsaMember == true).ToList();
        }
        public override void Delete(ClubMember entity)
        {
            entity.IsaMember = false;
            Update(entity);
        }
    }
}
