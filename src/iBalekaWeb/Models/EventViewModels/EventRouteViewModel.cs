using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public EventRouteViewModel(int eventRouteId, int eventId, int routeId, string dateAdded, double distance,string title,string description)
        {
            EventRouteId = eventRouteId;
            EventId = eventId;
            RouteId = routeId;
            DateAdded = dateAdded;
            Distance = distance;
            Title = title;
            Description = description;
        }

        public EventRouteViewModel(Route evntRoute)
        {
            Title = evntRoute.Title;
            RouteId = evntRoute.RouteId;
            Distance = evntRoute.Distance;
            DateAdded = DateTime.Now.Date.ToString();
        }
        [DisplayName("Event Route")]
        public int EventRouteId { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Distance")]
        public double Distance { get; set; }
        [DisplayName("Date Added")]
        public string DateAdded { get; set; }
        [DisplayName("Date Modified")]
        public string DateModified { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Event")]
        public int EventId { get; set; }
        [DisplayName("Route")]
        public int RouteId { get; set; }

    }
}
