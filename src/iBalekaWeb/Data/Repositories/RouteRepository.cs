using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.MapViewModels;

namespace iBalekaWeb.Data.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
        void AddRoute(CheckpointViewModel[] checkpoints, int totalDistance);
        void UpdateRoute(Route route, Checkpoint[] checkpoints);
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public void AddRoute(CheckpointViewModel[] Checkpoints,int totalDistance)
        {
            ICollection<Checkpoint> savingChecks = null;
            foreach (CheckpointViewModel chps in Checkpoints)
            {
                Checkpoint checks = new Checkpoint();
                checks.Deleted = false;
                savingChecks.Append(checks);
            }            
            Route savingRoute = new Route();
            savingRoute.DateRecorded = DateTime.Now.Date;
            savingRoute.Deleted = false;
            savingRoute.Checkpoint = savingChecks;
            savingRoute.Distance = totalDistance;
            //create map image?
            DbContext.Route.Add(savingRoute);
        }
        public void UpdateRoute(Route entity, Checkpoint[] Checkpoints)
        {
            IEnumerable<Checkpoint> checkpoints = GetCheckpoints(entity.RouteId);
            DeleteCheckPoints(checkpoints);
            foreach (Checkpoint chp in Checkpoints)
            {
                chp.RouteId = entity.RouteId;
                chp.Deleted = false;
            }
            entity.DateModified = DateTime.Now.Date;
            DbContext.Checkpoint.AddRange(Checkpoints);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public Route GetRouteByID(int id)
        {
            return DbContext.Route.Single(m => m.RouteId == id && m.Deleted == false);
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteId == id).ToList();
        }
        public override IEnumerable<Route> GetAll()
        {
            return DbContext.Route.Where(a => a.Deleted == false).ToList();
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
            Route deletedRoute = DbContext.Route.SingleOrDefault(x => x.RouteId == entity.RouteId);
            if (deletedRoute != null)
            {

                deletedRoute.Deleted = true;
                DbContext.Entry(deletedRoute).State = EntityState.Modified;

            }
        }
    }
}

