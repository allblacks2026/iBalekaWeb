using System;
using System.Collections.Generic;
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
        public int RouteId { get; set; }
        public string DateModified { get; set; }
        public string DateRecorded { get; set; }
        public string Title { get; set; }
        public string UserID {get;set;}
        public List<CheckpointViewModel> Checkpoints { get; set; }
        public double TotalDistance { get; set; }
    }
}
