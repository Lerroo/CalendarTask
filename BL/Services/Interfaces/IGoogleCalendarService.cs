using CalendarProj.DAO.Models;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarProj.BL.Services.Interfaces
{
    public interface IGoogleCalendarService
    {
        IList<Event> GetEvents();
        void CreateEvent(CustomEvent customEvent);
        List<SelectListItem> GetAllTypes();
        bool IsFreeTime(CustomEvent customEvent);
    }
}
