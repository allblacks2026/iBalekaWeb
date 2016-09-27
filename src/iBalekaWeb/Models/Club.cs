using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBalekaWeb.Models
{
    public class Club
    {
        public Club()
        {
            ClubMember = new HashSet<ClubMember>();
            Event = new HashSet<Event>();
        }
        [Key]
        public int ClubId { get; set; }
        public string DateCreated { get; set; }
        public bool Deleted { get; set; }
        [Required(ErrorMessage = " Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = " Location is Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = " Name is Required")]

        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
