using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public partial class AspNetUserTokens
    {
<<<<<<< HEAD
=======
        [Key]
>>>>>>> 2eeb5df7c2a4fb675c37dd2314e37c1618036a6e
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
