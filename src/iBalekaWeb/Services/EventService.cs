using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaWeb.Services
{
    public interface IEventService
    {
        void AddEventRoute(EventRoute route);
        Event GetEventByID(int id);
        IEnumerable<EventRoute> GetEventRoute(int id);
        IEnumerable<Event> GetAll();
        void AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void Delete(Event evnt);
        void SaveEvent();
    }
    public class EventService:IEventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly IUnitOfWork unitOfWork;

        public EventService(IEventRepository _repo,IUnitOfWork _unitOfWork)
        {
            _eventRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public Event GetEventByID(int id)
        {
            return _eventRepo.GetEventByID(id);
        }
        public IEnumerable<Event> GetAll()
        {
            return _eventRepo.GetAll();
        }
        public IEnumerable<EventRoute> GetEventRoute(int id)
        {
            return _eventRepo.GetEventRoute(id);
        }
        public void AddEventRoute(EventRoute route)
        {
            _eventRepo.AddEventRoute(route);
        }
        public void AddEvent(Event evnt)
        {
            _eventRepo.Add(evnt);
        }
        public void UpdateEvent(Event evnt)
        {
            _eventRepo.Update(evnt);
        }
        public void Delete(Event evnt)
        {
            _eventRepo.Delete(evnt);
        }
        public void SaveEvent()
        {
            unitOfWork.Commit();
        }
    }
}
