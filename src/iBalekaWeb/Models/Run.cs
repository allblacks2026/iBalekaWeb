using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public enum RunType
    {
        Personal,
        Event
    }
    public class Run
    {
        [Key]
        public int RunId { get; set; }
        public int AthleteId { get; set; }
        public double CaloriesBurnt { get; set; }
        public double Distance { get; set; }
        public DateTime DateRecorded { get; set; }
        public bool Deleted { get; set; }
        public DateTime EndTime { get; set; }
        public RunType RunType { get; set; }
        public int? EventId { get; set; }
        public int? RouteId { get; set; }
        public DateTime StartTime { get; set; }

        public virtual Rating Rating { get; set; }
        //[JsonIgnore]
        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
        public virtual Route Route { get; set; }
    }
}
