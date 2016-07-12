using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserTokens:IdentityUserToken<string>
    {

        

        public override string UserId { get; set; }
        public override string LoginProvider { get; set; }
        public override string Name { get; set; }
        public override string Value { get; set; }
    }
}
