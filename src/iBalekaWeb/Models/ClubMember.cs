using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public enum ClubStatus
    {
        Joined,
        Left
    }
    public class ClubMember
    {
        [Key]
        public int MemberId { get; set; }
        public int AthleteId { get; set; }
        public int ClubId { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public ClubStatus Status { get; set; }
        public virtual Athlete Athlete { get; set; }
        public virtual Club Club { get; set; }
    }
}
