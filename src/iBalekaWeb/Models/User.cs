using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class User:IdentityUser
    {
        public User()
        {
            Club = new HashSet<Club>();
        }

        public int UserId { get; set; }
        public string Country { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Club> Club { get; set; }
    }
}
