using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.EventViewModels
{
    public class EventRouteViewModel
    {
       

        public EventRouteViewModel() { }
        public EventRouteViewModel(int evntRouteId,int evntId,int routeId,string descript,string dateAdd,double distance,string title)
        {
            EventRouteId = evntRouteId;
            EventId = evntId;
            RouteId = routeId;
            Description = descript;
            Distance = distance;
            DateAdded = dateAdd;


        }
        public EventRouteViewModel( int routeId, string dateAdd, double distance, string title)
        {
            Title = title;
            RouteId = routeId;
            Distance = distance;
            DateAdded = dateAdd;


        }

        public EventRouteViewModel(int eventRouteId, int eventId, int routeId, string dateAdded, double distance)
        {
            EventRouteId = eventRouteId;
            EventId = eventId;
            RouteId = routeId;
            DateAdded = dateAdded;
            Distance = distance;
        }

        public EventRouteViewModel(Route evntRoute)
        {
            Title = evntRoute.Title;
            RouteId = evntRoute.RouteId;
            Distance = evntRoute.Distance;
            DateAdded = DateTime.Now.Date.ToString();
        }

        public int EventRouteId { get; set; }
        public string Title { get; set; }
        public double Distance { get; set; }
        public string DateAdded { get; set; }
        public string DateModified { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
        public int RouteId { get; set; }

    }
}
