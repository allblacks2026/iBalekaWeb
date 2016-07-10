using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class ClubMember
    {
        public int MemberId { get; set; }
        public int AthleteId { get; set; }
        public int ClubId { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public bool IsaMember { get; set; }

        public virtual Athlete Athlete { get; set; }
        public virtual Club Club { get; set; }
    }
}
