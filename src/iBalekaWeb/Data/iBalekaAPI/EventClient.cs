using iBalekaWeb.Data.iBalekaAPI;
using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.EventViewModels;
using iBalekaWeb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.iBalekaAPI
{
    public interface IEventClient
    {
        SingleModelResponse<Event> SaveEvent(EventViewModel evnt);
        SingleModelResponse<Event> UpdateEvent(EventViewModel evnt);
        SingleModelResponse<Event> GetEvent(int eventId);
        ListModelResponse<Event> GetEvents();
        ListModelResponse<Event> GetUserEvents(string userId);
        SingleModelResponse<Event> DeleteEvent(int eventId);
    }
    public class EventClient : ApiClient,IEventClient
    {
        public const string EventUri = "Event/";
        public EventClient():base()
        {
        }

        public SingleModelResponse<Event> SaveEvent(EventViewModel evnt)
        {
            string saveUrl = EventUri + "SaveEvent";
            List<EventRoute> newRoutes = new List<EventRoute>();
            foreach (EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                route.Title = evntRoute.Title;
                route.Description = evntRoute.Description;
                route.RouteID = evntRoute.RouteId;
                route.EventID = evnt.EventId;
                newRoutes.Add(route);
            }

            var newEvent = new Event
            {
                UserID = evnt.UserID,
                Date = evnt.Date,
                Time = evnt.Time,
                Description = evnt.Description,
                Location = evnt.Location,
                Title = evnt.Title,
                EventRoute = newRoutes
            };
            var createdEvent = PostContent(saveUrl, newEvent);
            return createdEvent;
        }
        public SingleModelResponse<Event> UpdateEvent(EventViewModel evnt)
        {
            string saveUrl = EventUri + "Update/EditEvent";
            List<EventRoute> newRoutes = new List<EventRoute>();
            foreach (EventRouteViewModel evntRoute in evnt.EventRoutes)
            {
                EventRoute route = new EventRoute(DateTime.Now.ToString());
                route.Title = evntRoute.Title;
                route.Description = evntRoute.Description;
                route.RouteID = evntRoute.RouteId;
                route.EventID = evnt.EventId;
                newRoutes.Add(route);
            }

            var newEvent = new Event
            {
                EventId = evnt.EventId,
                Date = evnt.Date,
                Time = evnt.Time,
                Description = evnt.Description,
                Location = evnt.Location,
                Title = evnt.Title,
                EventRoute = newRoutes
            };
            var updatedEvent = PutContent(saveUrl, newEvent);
            return updatedEvent;
        }
        public SingleModelResponse<Event> GetEvent(int eventId)
        {
            string getUrl = EventUri + "DeleteEvent?evnt="+eventId;
            var evnt = GetSingleContent<Event>(getUrl);
            return evnt;
        }
        public ListModelResponse<Event> GetEvents()
        {
            string getUrl = EventUri + "GetEvents";
            var evnt = GetListContent<Event>(getUrl);
            return evnt;
        }
        public ListModelResponse<Event> GetUserEvents(string userId)
        {
            string getUrl = EventUri + "User/GetUserEvents?userId="+userId;
            var evnt = GetListContent<Event>(getUrl);
            return evnt;
        }
        public SingleModelResponse<Event> DeleteEvent(int eventId)
        {
            string getUrl = EventUri + "DeleteEvent?eventId="+eventId;
            var devnt = DeleteContent<Event>(getUrl,eventId);
            return devnt;
        }
    }
}
