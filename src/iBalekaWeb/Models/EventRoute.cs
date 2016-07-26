using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class EventRoute
    {
        public EventRoute() { }
        public EventRoute(string dateAdded)
        {            
            
            DateAdded = dateAdded;
            Deleted = false;
        }
        public int EventRouteID { get; set; }
        public string DateAdded { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public int EventID { get; set; }
        public int RouteID { get; set; }

        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
