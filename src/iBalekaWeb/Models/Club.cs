using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBalekaWeb.Models
{
    public partial class Club
    {
        public Club()
        {
            ClubMember = new HashSet<ClubMember>();
            Event = new HashSet<Event>();
        }
        [Key]
        public int ClubId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "varchar(MAX)")]

        public string UserId { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
