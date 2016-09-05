using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public enum EventType
    {
        Open,
        Active,
        Closed
    }
    public class Event
    {
        public Event()
        {
           
        }
        public Event(Event evnt)
        {
            Date = evnt.Date;
            Description = evnt.Description;
            Title = evnt.Title;
            Time = evnt.Time;
            EventRoute = evnt.EventRoute;
            Location = evnt.Location;
        }
        public Event(int evntId, string userId, string descript, string title, string location, List<EventRoute> evntRoutes, string eventDate, string eventTime, string dateAdded, string dateModified)
        {
            EventId = evntId;
            UserID = userId;
            Date = eventDate;
            Time = eventTime;
            DateCreated = dateAdded;
            Description = descript;
            Title = title;
            Location = location;
            EventRoute = evntRoutes;
            DateModified = dateModified;
            EventStatus = EventType.Open;
            Deleted = false;
        }



        [Key]
        public int EventId { get; set; }
        public string UserID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DateModified { get; set; }
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public EventType EventStatus { get; set; }
        public bool Deleted { get; set; }
        public int ClubID { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<EventRoute> EventRoute { get; set; }


    }
}
