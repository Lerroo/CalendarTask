
using CalendarProj.BL.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CalendarProj.DAO.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CalendarProj.DAO.Models.Enums;

namespace CalendarProj.BL.Services
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly string _applicationName = "Google Calendar API .NET Quickstart";
        private readonly string[] _scopes = { CalendarService.Scope.Calendar };
        private readonly string _currentCalendarId = "primary";

        private readonly CalendarService _calendarService;

        public GoogleCalendarService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            _calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        private string GenerateCalenraId()
        {
            return Guid.NewGuid().ToString();
        }

        public List<SelectListItem> GetAllTypes()
        {
            var years = new List<SelectListItem>();
            var i = 0;
            foreach (var typeMetting in Enum.GetValues(typeof(TypeMetting)))
            {            
                var newItem = new SelectListItem { Text = typeMetting.ToString(), Value = i.ToString() };
                years.Add(newItem);

                i += 1;
            }

            return years;
        }

        public bool IsFreeBusy(CustomEvent customEvent)
        {
            var allEvents = GetEvents();
            foreach (var eventItem in allEvents)
            {
                var start = eventItem.Start.DateTime;
                var end = eventItem.End.DateTime;
                var checkEnevt = customEvent.Start.DateTime;

                if (start > checkEnevt && checkEnevt<end)
                {
                    return false;
                }
            }
            return true;
        }

        public IList<Event> GetEvents()
        {          
            EventsResource.ListRequest request = _calendarService.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            IList<Event> _googleEvents = events.Items;
            return _googleEvents;
        }

        public void CreateEvent(CustomEvent customEvent)
        {
            Event newEvent = new Event()
            {
                Summary = customEvent.Summary,
                Start = customEvent.Start,
                End = customEvent.End
            };

            _calendarService.Events
                .Insert(newEvent, _currentCalendarId).
                .Execute();
        }

    }
}
