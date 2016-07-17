using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Models;
using iBalekaWeb.Models.MapViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace iBalekaWeb.Services
{
    public interface IRouteService
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetRoutes();
        void AddRoute(RouteViewModel route);
        void UpdateRoute(RouteViewModel route);
        void DeleteRoute(Route route);
        void SaveRoute();
    }

    public class RouteService:IRouteService
    {
        private readonly IRouteRepository _routeRepo;
        private readonly IUnitOfWork unitOfWork;

        public RouteService(IRouteRepository _repo, IUnitOfWork _unitOfWork)
        {
            _routeRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return _routeRepo.GetCheckpoints(id);
        }
        public IEnumerable<Route> GetRoutes()
        {
            return _routeRepo.GetAll();
        }
        public Route GetRouteByID(int id)
        {
            return _routeRepo.GetRouteByID(id);
        }
        public void AddRoute(RouteViewModel route)
        { 
            _routeRepo.AddRoute(route);
        }
        public void UpdateRoute(RouteViewModel route)
        {
            _routeRepo.UpdateRoute(route);
        }
        public void DeleteRoute(Route route)
        {
            _routeRepo.Delete(route);
        }
        public void SaveRoute()
        {
            unitOfWork.Commit();
        }
    }
}

