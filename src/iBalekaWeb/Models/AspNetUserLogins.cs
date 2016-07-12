using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserLogins:IdentityUserLogin<string>
    {
       
        public int Id { get; set; }
        public override string LoginProvider { get; set; }
        public override string ProviderKey { get; set; }
        public override string ProviderDisplayName { get; set; }
        public override string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
