using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.MapViewModels;
using iBalekaWeb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.iBalekaAPI
{
    public interface IMapClient
    {
        SingleModelResponse<Route> SaveRoute(RouteViewModel mapRoute, string userId);
        SingleModelResponse<Route> UpdateRoute(RouteViewModel mapRoute);
        SingleModelResponse<Route> GetRoute(int routeId);
        ListModelResponse<Route> GetRoutes();
        SingleModelResponse<Route> DeleteRoute(int routeId);
        ListModelResponse<Route> GetUserRoutes(string userId);
    }
    public class MapClient:ApiClient,IMapClient
    {
        public const string MapUri = "Map/";
        public MapClient() : base()
        {
        }

        public SingleModelResponse<Route> SaveRoute(RouteViewModel mapRoute,string userId)
        {
            string MapUrl = MapUri + "AddRoute?userId="+userId;
            List<Checkpoint> newCheckPoints = new List<Checkpoint>();
            foreach (CheckpointViewModel check in mapRoute.Checkpoints)
            {
                Checkpoint checkPoint = new Checkpoint();
                checkPoint.Latitude = check.Latitude;
                checkPoint.Longitude = check.Longitude;
                newCheckPoints.Add(checkPoint);
            }

            var newRoute = new Route
            {
                UserID = mapRoute.UserID,
                Title = mapRoute.Title,
                Distance = mapRoute.TotalDistance,
                Checkpoint = newCheckPoints
            };
            var createdRoute = PostContent(MapUrl, newRoute);
            return createdRoute;
        }
        public SingleModelResponse<Route> UpdateRoute(RouteViewModel mapRoute)
        {
            string MapUrl = MapUri + "Update/EditRoute";
            List<Checkpoint> newCheckPoints = new List<Checkpoint>();
            foreach (CheckpointViewModel check in mapRoute.Checkpoints)
            {
                Checkpoint checkPoint = new Checkpoint();
                checkPoint.Latitude = check.Latitude;
                checkPoint.Longitude = check.Longitude;
                newCheckPoints.Add(checkPoint);
            }

            var newRoute = new Route
            {
                RouteId = mapRoute.RouteId,
                UserID = mapRoute.UserID,
                Title = mapRoute.Title,
                Distance = mapRoute.TotalDistance,
                Checkpoint = newCheckPoints
            };
            var createdRoute = PutContent(MapUrl, newRoute);
            return createdRoute;
        }
        public SingleModelResponse<Route> GetRoute(int routeId)
        {
            string getUrl = MapUri + "GetRoute?routeId=" + routeId;
            var routes = GetSingleContent<Route>(getUrl);
            return routes;
        }
        
        public ListModelResponse<Route> GetRoutes()
        {
            string getUrl = MapUri + "GetRoutes";
            var routes = GetListContent<Route>(getUrl);
            return routes;
        }
        public ListModelResponse<Route> GetUserRoutes(string userId)
        {
            string getUrl = MapUri + "User/GetUserRoutes?userId=" + userId;
            var routes = GetListContent<Route>(getUrl);
            return routes;
        }
        public SingleModelResponse<Route> DeleteRoute(int routeId)
        {
            string getUrl = MapUri + "Delete/DeleteRoute?routeId="+routeId;
            var route = DeleteContent<Route>(getUrl,routeId);
            return route;
        }

    }
}
