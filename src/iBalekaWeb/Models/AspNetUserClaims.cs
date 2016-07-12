using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserClaims:IdentityUserClaim<string>
    {
       
        public override int Id { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }
        public override string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
