using iBalekaWeb.Data.Infastructure;
using iBalekaWeb.Data.Repositories;
using iBalekaWeb.Models;
using iBalekaWeb.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaWeb.Services
{
    public interface IEventService
    {
        Event GetEventByID(int id);
        EventViewModel GetEventByIDView(int id);
        IEnumerable<EventRoute> GetEventRoutes(int id);
        IEnumerable<Event> GetEvents(string userId);
        void AddEvent(EventViewModel evnt);

        void UpdateEvent(EventViewModel evnt);
        void Delete(Event evnt);
        void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute);
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
        public EventViewModel GetEventByIDView(int id)
        {
            return _eventRepo.GetEventByIDView(id);
        }
        public IEnumerable<Event> GetEvents(string userId)
        {
            return _eventRepo.GetEvents(userId);
        }
        public IEnumerable<EventRoute> GetEventRoutes(int id)
        {
            return _eventRepo.GetEventRoutes(id);
        }
        public void AddEvent(EventViewModel evnt)
        {
            _eventRepo.AddEvent(evnt);
        }
       
        public void UpdateEvent(EventViewModel evnt)
        {
            _eventRepo.UpdateEvent(evnt);
        }
        public void Delete(Event evnt)
        {
            _eventRepo.Delete(evnt);
        }
        public void DeleteEventRoutes(IEnumerable<EventRoute> evntRoute)
        {
            _eventRepo.DeleteEventRoutes(evntRoute);
        }

        public void SaveEvent()
        {
            unitOfWork.Commit();
        }
    }
}
