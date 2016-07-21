using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class EventRoute
    {
        public EventRoute(string descript,int evntId,int routeId,DateTime dateAdded)
        {
            Description = descript;
            EventId = evntId;
            RouteId = routeId;
            DateAdded = dateAdded;
            Deleted = false;
        }
        public int EventRouteId { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
        public int RouteId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
