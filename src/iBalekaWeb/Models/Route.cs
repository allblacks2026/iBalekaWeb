using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public class Route
    {
        public Route()
        {
            Checkpoint = new HashSet<Checkpoint>();
            EventRoute = new HashSet<EventRoute>();
            Rating = new HashSet<Rating>();
            Run = new HashSet<Run>();
            DateRecorded = DateTime.Now;
            DateModified = DateTime.Now;
            Deleted = false;
        }
        public Route(int routeId, string title, string userId, double distance, List<Checkpoint> checks, DateTime dateRecorded, DateTime dateModified)
        {
            RouteId = routeId;
            Title = title;
            UserID = userId;
            Distance = distance;
            Checkpoint = checks;
            DateRecorded = dateRecorded;
            DateModified = dateModified;
        }
        public int RouteId { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateRecorded { get; set; }
        public bool Deleted { get; set; }
        public double Distance { get; set; }
        public string MapImage { get; set; }

        public virtual ICollection<Checkpoint> Checkpoint { get; set; }
        public virtual ICollection<EventRoute> EventRoute { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
        public virtual ICollection<Run> Run { get; set; }
    }
}
