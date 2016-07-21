using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.EventViewModels
{
    public class EventViewModel
    { 
        public EventViewModel() { }
        public EventViewModel(int evntId,string userId,string descript,string title,string location,List<EventRouteViewModel> evntRoutes, string eventDate,string eventTime, DateTime dateAdded, DateTime dateModified)
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
        public int EventId { get; set; }
        public string UserID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public List<EventRouteViewModel> EventRoutes { get; set; }

    }
}
