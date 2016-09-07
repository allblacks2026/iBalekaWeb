using iBalekaWeb.Models;
using iBalekaWeb.Models.EventViewModels;
using iBalekaWeb.Models.MapViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.Extensions
{
    public static class ModelMapper
    {
        public static EventViewModel ToEventViewModel(this Event evnt)
        {
            List<EventRouteViewModel> evntRoutes = new List<EventRouteViewModel>();
            foreach (EventRoute route in evnt.EventRoute)
            {
                evntRoutes.Add(new EventRouteViewModel
                {
                    EventRouteId = route.EventRouteID,
                    Title = route.Title,
                    Description = route.Description,
                    RouteId = route.RouteID,
                    EventId = evnt.EventId,
                    
                    DateAdded = route.DateAdded                    
                });
            }
            return new EventViewModel
            {
                EventId = evnt.EventId,
                UserID = evnt.UserID,
                Date = evnt.Date,
                Time = evnt.Time,
                Description = evnt.Description,
                Location = evnt.Location,
                Title = evnt.Title,
                EventRoutes = evntRoutes
            };
        }
        public static RouteViewModel ToRouteViewModel(this Route route)
        {
            List<CheckpointViewModel> checks = new List<CheckpointViewModel>();
            foreach(Checkpoint ch in route.Checkpoint)
            {
                checks.Add(new CheckpointViewModel
                {
                    CheckpointId = ch.CheckpointId,
                    Latitude = ch.Latitude,
                    Longitude = ch.Longitude
                });
            }
            return new RouteViewModel
            {
                RouteId = route.RouteId,
                Title = route.Title,
                TotalDistance = route.Distance,
                UserID = route.UserID,
                Checkpoints = checks,
                DateRecorded = route.DateRecorded,
                DateModified = route.DateModified
                
            };
        }
        public static EventRouteViewModel ToEventRouteViewModel(this Route route)
        {
            List<CheckpointViewModel> checks = new List<CheckpointViewModel>();
            foreach (Checkpoint ch in route.Checkpoint)
            {
                checks.Add(new CheckpointViewModel
                {
                    CheckpointId = ch.CheckpointId,
                    Latitude = ch.Latitude,
                    Longitude = ch.Longitude
                });
            }
            return new EventRouteViewModel
            {
                RouteId = route.RouteId,
                Title = route.Title,
                Distance = route.Distance,
                DateAdded = DateTime.Now.ToString()
                
            };
        }
        public static List<EventViewModel> ToEventViewModels(this IEnumerable<Event> evnts)
        {
            List<EventViewModel> evntModel = new List<EventViewModel>();
            foreach (Event evnt in evnts)
            {
                List<EventRouteViewModel> evntRoutes = new List<EventRouteViewModel>();
                foreach (EventRoute route in evnt.EventRoute)
                {
                    evntRoutes.Add(new EventRouteViewModel
                    {
                        EventRouteId = route.EventRouteID,
                        Title = route.Title,
                        Description = route.Description,
                        RouteId = route.RouteID,
                        EventId = evnt.EventId,
                        Distance = route.Distance,
                        DateAdded = route.DateAdded
                    });
                }
                evntModel.Add(new EventViewModel
                {
                    EventId = evnt.EventId,
                    UserID = evnt.UserID,
                    Date = evnt.Date,
                    Time = evnt.Time,
                    Description = evnt.Description,
                    Location = evnt.Location,
                    Title = evnt.Title,
                    EventRoutes = evntRoutes
                });
            }
            return evntModel;
        }
        public static List<RouteViewModel> ToRouteViewModels(this IEnumerable<Route> routes)
        {
            List<RouteViewModel> routesModel = new List<RouteViewModel>();
            foreach(Route route in routes)
            {
                List<CheckpointViewModel> checks = new List<CheckpointViewModel>();
                foreach (Checkpoint ch in route.Checkpoint)
                {
                    checks.Add(new CheckpointViewModel
                    {
                        CheckpointId = ch.CheckpointId,
                        Latitude = ch.Latitude,
                        Longitude = ch.Longitude
                    });
                }
                routesModel.Add(new RouteViewModel
                {
                    RouteId = route.RouteId,
                    Title = route.Title,
                    TotalDistance = route.Distance,
                    UserID = route.UserID,
                    Checkpoints = checks,
                    DateRecorded = route.DateRecorded,
                    DateModified = route.DateModified

                });
            }
            return routesModel;
        }
    }
}
