using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class EventRegistration
    {
        public int RegistrationId { get; set; }
        public bool Arrived { get; set; }
        public int AthleteId { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool Deleted { get; set; }
        public int EventId { get; set; }
        public int SelectedRoute { get; set; }

        public virtual Athlete Athlete { get; set; }
        public virtual Event Event { get; set; }
    }
}
