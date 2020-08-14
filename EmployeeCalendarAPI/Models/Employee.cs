using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeCalendarAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Surname { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public List<CalendarEvent> CalendarEvents { get; set; }
    }
}
