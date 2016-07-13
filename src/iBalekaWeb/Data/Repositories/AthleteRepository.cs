using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;

namespace iBalekaWeb.Data.Repositories
{
    public interface IAthleteRepository:IRepository<Athlete>
    {
        Athlete GetAthleteByID(int id);
    }
    public class AthleteRepository:RepositoryBase<Athlete>,IAthleteRepository
    {
        public AthleteRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public Athlete GetAthleteByID(int id)
        {
            return DbContext.Athlete.Where(a => a.AthleteId == id && a.Deleted == false).FirstOrDefault();
        }
        public override IEnumerable<Athlete> GetAll()
        {
            return DbContext.Athlete.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(Athlete entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

    }
}
