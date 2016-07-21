using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.EventViewModels
{
    public class EventRouteViewModel
    {
        public EventRouteViewModel() { }
        public EventRouteViewModel(int evntRouteId,int evntId,int routeId,string descript,DateTime dateAdd)
        {
            EventRouteId = evntRouteId;
            EventId = evntId;
            RouteId = routeId;
            Description = descript;
            DateAdded = dateAdd;

        }
        public int EventRouteId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
        public int RouteId { get; set; }

    }
}
