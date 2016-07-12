using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class AspNetRoles:IdentityRole
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }

        public override string Id { get; set; }
        public override string ConcurrencyStamp { get; set; }
        public override string Name { get; set; }
        public override string NormalizedName { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
