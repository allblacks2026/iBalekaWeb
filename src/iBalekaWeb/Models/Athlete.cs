using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    /// <summary>
    /// Represents an athlete
    /// </summary>
    public class Athlete
    {
        public Athlete()
        {
            ClubMember = new HashSet<ClubMember>();
            EventRegistration = new HashSet<EventRegistration>();
            Run = new HashSet<Run>();
        }
        public int AthleteId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int? Gender { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<Run> Run { get; set; }
    }
}
