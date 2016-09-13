using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Route")]
        public int[] RouteId { get; set; }
        [DisplayName("Event")]
        public int EventId { get; set; }
        [DisplayName("Club Name")]
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        [DisplayName("User ID")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Event Date is Required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Event Time is Required")]
        public string Time { get; set; }
        [DisplayName("Date Modified")]
        public string DateModified { get; set; }
        [DisplayName("Date Created ")]
        public string DateCreated { get; set; }
        [Required(ErrorMessage = "Event Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Event Location is Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Event Title is Required")]
        public string Title { get; set; }
        [DisplayName("Event Routes")]
        public List<EventRouteViewModel> EventRoutes { get; set; }
        
    }
}
