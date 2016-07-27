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
        private IRouteRepository _routeRepo;
        public EventRepository(IDbFactory dbFactory, IRouteRepository repo)
            : base(dbFactory)
        {
            _routeRepo = repo;
        }
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


            //Event savedEvent = DbContext.Event.Single(x => x.UserID == newEvent.UserID && x.Title == newEvent.Title && x.DateCreated == newEvent.DateCreated && x.Deleted == false);
            foreach (EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                //DbContext.EventRoute.Add(route);
                route.Title = evntRoute.Title;
                route.Description = evnt.Description; //evntRoute.Description;
                route.RouteID = evntRoute.RouteId;
                newEvent.EventRoute.Add(route);
            }
            DbContext.Event.Add(newEvent);
            DbContext.SaveChanges();
        }

        public void UpdateEvent(EventViewModel evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventId);
            Event newEvent = GetEventByID(evnt.EventId);
            newEvent.Title = evnt.Title;
            newEvent.Description = evnt.Description;
            newEvent.Date = evnt.Date;
            newEvent.Time = evnt.Time;
            newEvent.Location = evnt.Location;
            newEvent.DateModified = DateTime.Now.ToString();


            //DeleteEventRoutes(evntRoutes);
            foreach(EventRoute route in evntRoutes)
            {
                DbContext.Entry(route).State = EntityState.Deleted;
            }
            DbContext.SaveChanges();
            newEvent.EventRoute = new List<EventRoute>();
            foreach (EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(evntRoute.DateAdded);
                route.Title = evntRoute.Title;
                route.Description = evnt.Description; 
                route.RouteID = evntRoute.RouteId;
                newEvent.EventRoute.Add(route);
            }


            DbContext.Event.Update(newEvent);
        }
        public void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute)
        {
            foreach (EventRoute route in evntRoute)
            {
                route.Deleted = true;
                DbContext.Entry(route).State = EntityState.Modified;
            }
        }
        public void DeleteEventRoute(EventRoute evntRoute)
        {

            evntRoute.Deleted = true;
            DbContext.Entry(evntRoute).State = EntityState.Modified;

        }
        public IEnumerable<EventRoute> GetEventRoutes(int id)
        {
            return DbContext.EventRoute.Where(m => m.EventID == id && m.Deleted == false).ToList();
        }
        public Event GetEventByID(int id)
        {
            return DbContext.Event.Single(m => m.EventID == id && m.Deleted == false);
        }
        public EventViewModel GetEventByIDView(int id)
        {
            Event loadEvent = GetEventByID(id);
            List<EventRoute> loadEventRoutes = GetEventRoutes(id).ToList();
            List<EventRouteViewModel> eventRoutes = new List<EventRouteViewModel>();
            foreach (EventRoute evntRoute in loadEventRoutes)
            {
                Route route = _routeRepo.GetRouteByID(evntRoute.RouteID);

                eventRoutes.Add(new EventRouteViewModel(evntRoute.EventRouteID, evntRoute.EventID, evntRoute.RouteID, evntRoute.DateAdded, route.Distance, evntRoute.Title, evntRoute.Description));
            }
            EventViewModel eventViewModel = new EventViewModel(loadEvent.EventID, loadEvent.UserID, loadEvent.Description, loadEvent.Title, loadEvent.Location, eventRoutes, loadEvent.Date, loadEvent.Time, loadEvent.DateCreated, loadEvent.DateModified);
            return eventViewModel;
        }
        public IEnumerable<Event> GetEvents(string id)
        {
            return DbContext.Event.Where(a => a.Deleted == false && a.UserID == id);
        }
        public override void Delete(Event evnt)
        {
            IEnumerable<EventRoute> evntRoutes = GetEventRoutes(evnt.EventID);
            if (evntRoutes != null)
            {
                foreach (EventRoute route in evnt.EventRoute)
                {
                    route.Deleted = true;
                    DbContext.Entry(route).State = EntityState.Modified;
                }
            }
            Event deletedEvent = DbContext.Event.Single(x => x.EventID == evnt.EventID);
            if (deletedEvent != null)
            {
                deletedEvent.Deleted = true;
                DbContext.Entry(deletedEvent).State = EntityState.Modified;
            }
        }
    }
}
