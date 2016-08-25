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
        Task<SingleModelResponse<Route>> SaveRoute(RouteViewModel mapRoute);
        Task<SingleModelResponse<Route>> UpdateRoute(RouteViewModel mapRoute);
        Task<SingleModelResponse<Route>> GetRoute(int routeId);
        Task<ListModelResponse<Route>> GetRoutes();
        Task<SingleModelResponse<Route>> DeleteRoute(int routeId);
        Task<ListModelResponse<Route>> GetUserRoutes(string userId);
    }
    public class MapClient:ClientBase,IMapClient
    {
        public const string MapUri = "Map/";
        public MapClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<SingleModelResponse<Route>> SaveRoute(RouteViewModel mapRoute)
        {
            string MapUrl = MapUri + "AddRoute";
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
                Checkpoint = (ICollection<Checkpoint>)newCheckPoints
            };
            var createdRoute = await PostEncodedContentWithResponse<SingleModelResponse<Route>, Route>(MapUrl, newRoute);
            return createdRoute;
        }
        public async Task<SingleModelResponse<Route>> UpdateRoute(RouteViewModel mapRoute)
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
                UserID = mapRoute.UserID,
                Title = mapRoute.Title,
                Distance = mapRoute.TotalDistance,
                Checkpoint = newCheckPoints
            };
            var createdRoute = await PutEncodedContentWithResponse<SingleModelResponse<Route>, Route>(MapUrl, newRoute);
            return createdRoute;
        }
        public async Task<SingleModelResponse<Route>> GetRoute(int routeId)
        {
            string getUrl = MapUri + "User/GetRoute?routeId=" + routeId;
            var routes = await GetSingleJsonEncodedContent<SingleModelResponse<Route>, Route>(getUrl);
            return routes;
        }
        
        public async Task<ListModelResponse<Route>> GetRoutes()
        {
            string getUrl = MapUri + "GetRoutes";
            var routes = await GetListJsonEncodedContent<ListModelResponse<Route>, Route>(getUrl);
            return routes;
        }
        public async Task<ListModelResponse<Route>> GetUserRoutes(string userId)
        {
            string getUrl = MapUri + "User/GetUserRoutes?userId=" + userId;
            var routes = await GetListJsonEncodedContent<ListModelResponse<Route>, Route>(getUrl);
            return routes;
        }
        public async Task<SingleModelResponse<Route>> DeleteRoute(int routeId)
        {
            string getUrl = MapUri + "Delete/DeleteRoute?userId=" + routeId;
            var route = await DeleteEncodedContentWithResponse<SingleModelResponse<Route>, Route>(getUrl);
            return route;
        }

    }
}
