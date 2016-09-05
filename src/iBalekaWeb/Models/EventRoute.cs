using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public class EventRoute
    {
        public EventRoute() { }
        public EventRoute(string dateAdded)
        {

            DateAdded = dateAdded;
            Deleted = false;
        }
        public EventRoute(int routeId, string dateAdd, string title)
        {
            Title = title;
            RouteID = routeId;
            DateAdded = dateAdd;


        }
        public EventRoute(int eventRouteId, int eventId, int routeId, string dateAdded, string title, string description)
        {
            EventRouteID = eventRouteId;
            EventID = eventId;
            RouteID = routeId;
            DateAdded = dateAdded;
            Title = title;
            Description = description;
        }
        public EventRoute(Route evntRoute)
        {
            Title = evntRoute.Title;
            RouteID = evntRoute.RouteId;
            DateAdded = DateTime.Now.Date.ToString();
        }
        public string Title { get; set; }
        public int EventRouteID { get; set; }
        public string DateAdded { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public int EventID { get; set; }
        public int RouteID { get; set; }
        //[JsonIgnore]
        public virtual Route Route { get; set; }
    }
}
