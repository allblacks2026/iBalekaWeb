using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Models.MapViewModels
{
    public class CheckpointViewModel
    {
        public CheckpointViewModel() { }
        public CheckpointViewModel(double lat,double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
