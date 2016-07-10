using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;

namespace iBalekaWeb.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        void AddEventRoute(EventRoute route);
        Event GetEventByID(int id);
        IEnumerable<EventRoute> GetEventRoute(int id);
    }
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        //add addEvent
        public void AddEventRoute(EventRoute route)
        {
            DbContext.EventRoute.Add(route);
        }
        public IEnumerable<EventRoute> GetEventRoute(int id)
        {
            return DbContext.EventRoute.Where(m => m.EventId == id && m.Deleted == false).ToList();
        }
        public Event GetEventByID(int id)
        {
            return DbContext.Event.Where(m => m.EventId == id && m.Deleted == false).SingleOrDefault();
        }
        public override IEnumerable<Event> GetAll()
        {
            return DbContext.Event.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(Event entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
    }
}
