using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.MapViewModels
{
    public class RouteViewModel
    {
        public RouteViewModel() { }
        public int RouteId { get; set; }
        public string Title { get; set; }
        public string UserID {get;set;}
        public List<CheckpointViewModel> Checkpoints { get; set; }
        public int TotalDistance { get; set; }
    }
}
