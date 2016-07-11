using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserLogins
    {
        [Key]
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public int UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
