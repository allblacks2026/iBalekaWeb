using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.MapViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace iBalekaWeb.Data.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route GetRouteByID(int id);
        RouteViewModel GetRouteByIDView(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetRoutes(string UserID);
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
        void AddRoute(RouteViewModel route);
        void UpdateRoute(RouteViewModel route);
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public RouteRepository(IDbFactory dbFactory,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
            : base(dbFactory)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            
        }
        

        public void AddRoute(RouteViewModel route)
        {
            Route savingRoute = new Route();
            savingRoute.UserID = route.UserID;
            savingRoute.Title = route.Title;
            savingRoute.Distance = route.TotalDistance;
            foreach (CheckpointViewModel chps in route.Checkpoints)
            {                
                Checkpoint checks = new Checkpoint(chps.Latitude, chps.Longitude);
                DbContext.Checkpoint.Add(checks);
                savingRoute.Checkpoint.Add(checks);
            }    
            
            
            //create map image?
            DbContext.Route.Add(savingRoute);
        }
        public void UpdateRoute(RouteViewModel updatedRoute)
        {
            IEnumerable<Checkpoint> checkpoints = GetCheckpoints(updatedRoute.RouteId);            
            DeleteCheckPoints(checkpoints);
            Route route = GetRouteByID(updatedRoute.RouteId);
            route.Checkpoint = null;
            foreach (CheckpointViewModel chp in updatedRoute.Checkpoints)
            {
                Checkpoint check = new Checkpoint(chp.Latitude, chp.Longitude);
                check.RouteId = updatedRoute.RouteId;
                DbContext.Checkpoint.Add(check);
                route.Checkpoint.Add(check);            
                
            }
            route.Distance = updatedRoute.TotalDistance;
            route.Title = updatedRoute.Title;
            route.DateModified = DateTime.Now.Date;
            
            DbContext.Route.Update(route);
        }
        public Route GetRouteByID(int id)
        {
            Route route = DbContext.Route.Single(m => m.RouteId == id && m.Deleted == false);
            return route;
        }
        public RouteViewModel GetRouteByIDView(int id)
        {
            Route route = GetRouteByID(id);
            List<Checkpoint> checks = GetCheckpoints(route.RouteId).ToList();
            List<CheckpointViewModel> checkViews = new List<CheckpointViewModel>();
            foreach (Checkpoint check in checks)
            {
                checkViews.Add(new CheckpointViewModel(check.Latitude, check.Longitude));
            }
            RouteViewModel viewRoute = new RouteViewModel(route.RouteId, route.Title, route.UserID, route.Distance, checkViews,route.DateRecorded,route.DateModified);

            return viewRoute;
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteId == id).ToList();
        }
        public IEnumerable<Route> GetRoutes(string UserID)
        {
            return DbContext.Route.Where(a =>a.UserID==UserID && a.Deleted == false).ToList();
        }
        public void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                DbContext.Entry(checkpoint).State = EntityState.Deleted;
            }
        }
        public override void Delete(Route entity)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(entity.RouteId);
            if (Checkpoints != null)
            {
                foreach (Checkpoint check in Checkpoints)
                {
                    check.Deleted = true;
                    DbContext.Entry(check).State = EntityState.Modified;
                }
            }
            Route deletedRoute = DbContext.Route.FirstOrDefault(x => x.RouteId == entity.RouteId);
            if (deletedRoute != null)
            {

                deletedRoute.Deleted = true;
                DbContext.Entry(deletedRoute).State = EntityState.Modified;

            }
        }
    }
}

