using iBalekaWeb.Data.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.iBalekaAPI
{
    public interface IEventRegistration
    {
        ListModelResponse<EventRegistration> GetEventRegistrations(int eventId);
    }
    public class EventRegistrationClient: ApiClient,IEventRegistration
    {
        public const string EventRegUri = "EventRegistration/";
        public EventRegistrationClient():base()
        {
        }

        public ListModelResponse<EventRegistration> GetEventRegistrations(int eventId)
        {
            string getUrl = EventRegUri + "Event/GetRegistrations?eventId="+eventId;
            var evnt = GetListContent<EventRegistration>(getUrl);
            return evnt;
        }
    }
}
