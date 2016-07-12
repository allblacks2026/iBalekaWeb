using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public partial class AspNetRoleClaims:IdentityRoleClaim<string>
    {
       
        public override int Id { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }
        public override string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
    }
}
