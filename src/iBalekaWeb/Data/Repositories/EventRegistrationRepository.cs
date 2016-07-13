using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.Repositories
{
    public interface IEventRegRepository : IRepository<EventRegistration>
    {
        EventRegistration GetEventRegByID(int id);
    }
    public class EventRegistrationRepository : RepositoryBase<EventRegistration>, IEventRegRepository
    {
        public EventRegistrationRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public EventRegistration GetEventRegByID(int id)
        {
            return DbContext.EventRegistration.Where(m => m.RegistrationId == id && m.Deleted == false).FirstOrDefault();
        }
        public override IEnumerable<EventRegistration> GetAll()
        {
            return DbContext.EventRegistration.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(EventRegistration entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
    }
}
