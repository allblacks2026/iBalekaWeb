using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.EventViewModels
{
    
    public class EventViewModel
    { 
        public EventViewModel(EventViewModel route)
        {
            Date = route.Date;
            Description = route.Description;
            DateCreated = route.DateCreated;
            DateModified = route.DateModified;
            Title = route.Title;
            Time = route.Time;
            EventRoutes = route.EventRoutes;

            Location = route.Location;
        }
        public EventViewModel(int evntId,string userId,string descript,string title,string location,List<EventRouteViewModel> evntRoutes, string eventDate,string eventTime, string dateAdded, string dateModified)
        {
            EventId = evntId;
            UserID = userId;
            Date = eventDate;
            Time = eventTime;
            DateCreated = dateAdded;
            Description = descript;
            Title = title;
            Location = location;
            EventRoutes = evntRoutes;
            DateModified = dateModified;
        }

        public EventViewModel()
        {
        }

        public int EventId { get; set; }
        public string UserID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DateModified { get; set; }
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public List<EventRouteViewModel> EventRoutes { get; set; }

    }
}
