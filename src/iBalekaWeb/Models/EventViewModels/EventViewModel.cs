using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models.EventViewModels
{
    public class EventViewModel
    {
        [Required]
        public string title { get; set; }

        [Required]
        public DateTime dateAndTime { get; set; }

        [Required]
        public DateTime dateCreated { get; set; }

        [Required]
        public string description { get; set; }
        
        [Required]
        public string location { get; set; }
    }
}
