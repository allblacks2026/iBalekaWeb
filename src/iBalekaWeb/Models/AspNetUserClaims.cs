using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserClaims
    {
        [Key]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public int UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
