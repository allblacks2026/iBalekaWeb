using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaWeb.Models
{
    public partial class Event
    {
        public Event()
        {
            EventRegistration = new HashSet<EventRegistration>();
            EventRoute = new HashSet<EventRoute>();
            Run = new HashSet<Run>();
            DateCreated = DateTime.Now.ToString();
            ClubID = 0;
            Deleted = false;
        }

        [Key]
        public int EventID { get; set; }
        public string UserID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string DateCreated { get; set; }
        public string DateModified { get; set; }
        public bool Deleted { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public int ClubID { get; set; }
        public virtual ICollection<EventRegistration> EventRegistration { get; set; }
        public virtual ICollection<EventRoute> EventRoute { get; set; }
        public virtual ICollection<Run> Run { get; set; }
      
    }
}
