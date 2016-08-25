using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models
{
    public class Error
    {
        public Error() { }
        public Error(string mes)
        {
            Message = mes;
        }
        public string Message { get; set; }
    }
}
