using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.EventViewModels;
using Microsoft.EntityFrameworkCore;

namespace iBalekaWeb.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        void AddEvent(EventViewModel evnt);
        void UpdateEvent(EventViewModel evnt);
        void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute);
        Event GetEventByID(int id);
        EventViewModel GetEventByIDView(int id);
        IEnumerable<EventRoute> GetEventRoutes(int id);
        IEnumerable<Event> GetEvents(string userId);
    }
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        //add addEvent
        public void AddEvent(EventViewModel evnt)
        {
            Event newEvent = new Event();
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.UserID = evnt.UserID;
            foreach(EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(evntRoute.Description, evntRoute.EventId, evntRoute.RouteId, evntRoute.DateAdded);
                DbContext.EventRoute.Add(route);
                newEvent.EventRoute.Add(route);
            }
            DbContext.Event.Add(newEvent);
        }
        public void UpdateEvent(EventViewModel evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventId);
            DeleteEventRoutes(evntRoutes);
            Event newEvent = GetEventByID(evnt.EventId);
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.DateModified = DateTime.Now;
            newEvent.EventRoute = null;
            foreach (EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(evntRoute.Description, evntRoute.EventId, evntRoute.RouteId, evntRoute.DateAdded);
                DbContext.EventRoute.Add(route);
                newEvent.EventRoute.Add(route);
            }
            DbContext.Event.Update(newEvent);
        }
        public void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute)
        {
            foreach(EventRoute route in evntRoute)
            {
                route.Deleted = true;
                DbContext.Entry(route).State = EntityState.Modified;
            }
        }
        public IEnumerable<EventRoute> GetEventRoutes(int id)
        {
            return DbContext.EventRoute.Where(m => m.EventId == id && m.Deleted == false).ToList();
        }
        public Event GetEventByID(int id)
        {
            return DbContext.Event.Single(m => m.EventId == id && m.Deleted == false);
        }
        public EventViewModel GetEventByIDView(int id)
        {
            Event loadEvent = GetEventByID(id);
            List<EventRoute> loadEventRoutes = GetEventRoutes(id).ToList();
            List<EventRouteViewModel> eventRoutes = new List<EventRouteViewModel>();
            foreach (EventRoute evntRoute in loadEventRoutes)
            {
                eventRoutes.Add(new EventRouteViewModel(evntRoute.EventRouteId, evntRoute.EventId, evntRoute.RouteId, evntRoute.Description, evntRoute.DateAdded));
            }
            EventViewModel eventViewModel = new EventViewModel(loadEvent.EventId, loadEvent.UserID, loadEvent.Description, loadEvent.Title, loadEvent.Location, eventRoutes, loadEvent.Date,loadEvent.Time, loadEvent.DateCreated,loadEvent.DateModified);
            return eventViewModel;
        }
        public IEnumerable<Event> GetEvents(string id)
        {
            return DbContext.Event.Where(a => a.Deleted == false && a.UserID==id).ToList();
        }
        public override void Delete(Event evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventId);
            if(evntRoutes!=null)
            {
                foreach(EventRoute route in evnt.EventRoute)
                {
                    route.Deleted = true;
                    DbContext.Entry(route).State = EntityState.Modified;
                }
            }
            Event deletedEvent = DbContext.Event.Single(x => x.EventId == evnt.EventId);
            if(deletedEvent!=null)
            {
                deletedEvent.Deleted = true;
                DbContext.Entry(deletedEvent).State = EntityState.Modified;
            }
        }
    }
}
