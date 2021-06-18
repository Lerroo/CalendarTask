using CalendarProj.DAO.Models.Enums;
using FastTripApp.Validation;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarProj.DAO.Models
{
    public class CustomEvent
    {
        [Required]
        [DisplayName("Title")]
        public string Summary { get; set; }

        [Required]
        [CheckDateRangeAttribute]
        [DisplayName("Title")]
        public EventDateTime Start { get; set; }
        [Required]
        public EventDateTime End
        {
            get
            {
                if (Start.DateTime == null)
                {
                    return Start;
                }

                int addMinutes = 0;
                switch ((TypeMetting)TypeEnumInt)
                {
                    case TypeMetting.Short: 
                        addMinutes = 15;
                        break;

                    case TypeMetting.Long:
                        addMinutes = 60;
                        break;
                }

                EventDateTime eventDateTime = new EventDateTime
                {
                    DateTime = Start.DateTime.Value.AddMinutes(addMinutes)
                };
                return eventDateTime;
            }
        }      
        [DisplayName("Type metting")]
        public IEnumerable<SelectListItem> Types { get; set; }
        public int TypeEnumInt { get; set; }
    }
}
