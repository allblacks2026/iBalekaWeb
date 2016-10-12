using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.MapViewModels
{
   
    public class RouteViewModel
    {
        public RouteViewModel() { }
        public RouteViewModel(int routeId,string title,string userId,double distance,List<CheckpointViewModel> checks, string dateRecorded, string dateModified)
        {
            RouteId = routeId;
            Title = title;
            UserID = userId;
            TotalDistance = distance;
            Checkpoints = checks;
            DateRecorded = dateRecorded;
            DateModified = dateModified;
        }
        [DisplayName("Route")]

        public int RouteId { get; set; }

        [DisplayName("Date Modified")]
        public string DateModified { get; set; }

        [DisplayName("Date Recorded")]
        public string DateRecorded { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("User ID")]
        public string UserID {get;set;}

        [DisplayName("Checkpoints")]
        public List<CheckpointViewModel> Checkpoints { get; set; }

        [DisplayName("Total Distance")]
        public double TotalDistance { get; set; }
        public string Location { get; set; }
    }
}
