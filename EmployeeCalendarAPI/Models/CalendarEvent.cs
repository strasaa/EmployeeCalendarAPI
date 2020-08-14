using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeCalendarAPI.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }

        /* TODO
         * Add validations for StartTime and EndTime 
         * Validate ISO format
         * */

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        public string Note { get; set; }

        [Required]
        public int CalendarEventTypeId { get; set; }

        [JsonIgnore]
        public CalendarEventType CalendarEventType { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; }
    }
}
