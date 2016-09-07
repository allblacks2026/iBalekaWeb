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
            DateRecorded = DateTime.Now.ToString();
            DateModified = DateTime.Now.ToString();
            Deleted = false;
        }
        public Route(int routeId, string title, string userId, double distance, List<Checkpoint> checks, string dateRecorded, string dateModified)
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
        public string DateModified { get; set; }
        public string DateRecorded { get; set; }
        public bool Deleted { get; set; }
        public double Distance { get; set; }
        public string MapImage { get; set; }

        public virtual ICollection<Checkpoint> Checkpoint { get; set; }
        public virtual ICollection<EventRoute> EventRoute { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }
        public virtual ICollection<Run> Run { get; set; }
    }
}
