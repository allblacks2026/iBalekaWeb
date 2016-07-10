using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Deleted { get; set; }
        public int? RouteId { get; set; }
        public int RunId { get; set; }
        public int Value { get; set; }

        public virtual Route Route { get; set; }
        public virtual Run Run { get; set; }
    }
}
