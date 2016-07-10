using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class Athlete
    {
        public Athlete()
        {
            ClubMember = new HashSet<ClubMember>();
            EventRegistration = new HashSet<EventRegistration>();
            Run = new HashSet<Run>();
        }
        

        public int AthleteId { get; set; }
        public DateTime DateJoined { get; set; }
        public bool Deleted { get; set; }
        public string Firstname { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public string LicenseNo { get; set; }
        public string Surname { get; set; }
        public double? Weight { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<Run> Run { get; set; }
    }
}
