using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.ClubViewModels
{
    public class ClubViewModel
    {

        public ClubViewModel(int clubId, string dateCreated, bool deleted, string description,string name,string userid, string location, ICollection<ClubMember> clubMember, ICollection<Event> Eventt)
        {
            ClubId = clubId;
            DateCreated = dateCreated;
            Deleted = deleted;
            Description = description;
            Name = name;
            UserId = userid;
            Location = location;
            ClubMember = clubMember;
            Event = Eventt;
        }
        public ClubViewModel()
        {

        }


#pragma warning disable CS1701 // Assuming assembly reference matches identity
        [DisplayName("Club")]
#pragma warning restore CS1701 // Assuming assembly reference matches identity
        public int ClubId { get; set; }
        [DisplayName("Date Created")]
        public string DateCreated { get; set; }
        [DisplayName("Deleted")]
        public bool Deleted { get; set; }
        [Required(ErrorMessage = " Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = " Location is Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = " Name is Required")]

        public string Name { get; set; }
        [DisplayName("User")]
        public string UserId { get; set; }

        public virtual ICollection<ClubMember> ClubMember { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
