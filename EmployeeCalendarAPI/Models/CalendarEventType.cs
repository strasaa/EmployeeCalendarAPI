using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeCalendarAPI.Models
{
    public class CalendarEventType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }

        [JsonIgnore]
        public List<CalendarEvent> CalendarEvents { get; set; }

    }
}
