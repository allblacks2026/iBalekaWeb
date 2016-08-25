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
        Task<SingleModelResponse<Event>> SaveEvent(EventViewModel evnt);
        Task<SingleModelResponse<Event>> UpdateEvent(EventViewModel evnt);
        Task<SingleModelResponse<Event>> GetEvent(int eventId);
        Task<ListModelResponse<Event>> GetEvents();
        Task<ListModelResponse<Event>> GetUserEvents(string userId);
        Task<SingleModelResponse<Event>> DeleteEvent(int eventId);
    }
    public class EventClient : ClientBase,IEventClient
    {
        public const string EventUri = "Event/";
        public EventClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<SingleModelResponse<Event>> SaveEvent(EventViewModel evnt)
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
                EventRoute = (ICollection<EventRoute>)newRoutes
            };
            var createdEvent = await PostEncodedContentWithResponse<SingleModelResponse<Event>, Event>(saveUrl, newEvent);
            return createdEvent;
        }
        public async Task<SingleModelResponse<Event>> UpdateEvent(EventViewModel evnt)
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
                EventRoute = (ICollection<EventRoute>)newRoutes
            };
            var updatedEvent = await PutEncodedContentWithResponse<SingleModelResponse<Event>, Event>(saveUrl, newEvent);
            return updatedEvent;
        }
        public async Task<SingleModelResponse<Event>> GetEvent(int eventId)
        {
            string getUrl = EventUri + "DeleteEvent?evnt="+eventId;
            var evnt = await GetSingleJsonEncodedContent<SingleModelResponse<Event>, Event>(getUrl);
            return evnt;
        }
        public async Task<ListModelResponse<Event>> GetEvents()
        {
            string getUrl = EventUri + "GetEvents";
            var evnt = await GetListJsonEncodedContent<ListModelResponse<Event>, Event>(getUrl);
            return evnt;
        }
        public async Task<ListModelResponse<Event>> GetUserEvents(string userId)
        {
            string getUrl = EventUri + "User/GetUserEvents?userId="+userId;
            var evnt = await GetListJsonEncodedContent<ListModelResponse<Event>, Event>(getUrl);
            return evnt;
        }
        public async Task<SingleModelResponse<Event>> DeleteEvent(int eventId)
        {
            string getUrl = EventUri + "DeleteEvent?eventId=" + eventId;
            var devnt = await DeleteEncodedContentWithResponse<SingleModelResponse<Event>,Event>(getUrl);
            return devnt;
        }
    }
}
