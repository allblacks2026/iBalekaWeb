using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class Run
    {
        public int RunId { get; set; }
        public int AthleteId { get; set; }
        public double CaloriesBurnt { get; set; }
        public DateTime DateRecorded { get; set; }
        public bool Deleted { get; set; }
        public DateTime EndTime { get; set; }
        public int? EventId { get; set; }
        public int? RouteId { get; set; }
        public DateTime StartTime { get; set; }

        public virtual Rating Rating { get; set; }
        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
