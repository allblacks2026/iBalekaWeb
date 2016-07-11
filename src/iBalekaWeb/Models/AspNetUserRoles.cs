using System;
using System.Collections.Generic;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserRoles
    {
        public int UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
