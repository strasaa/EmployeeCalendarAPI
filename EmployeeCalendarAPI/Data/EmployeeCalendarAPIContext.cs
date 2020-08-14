using Microsoft.EntityFrameworkCore;

namespace EmployeeCalendarAPI.Data
{
    public class EmployeeCalendarAPIContext : DbContext
    {
        public EmployeeCalendarAPIContext (DbContextOptions<EmployeeCalendarAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeCalendarAPI.Models.Employee> Employees { get; set; }

        public DbSet<EmployeeCalendarAPI.Models.CalendarEvent> CalendarEvents { get; set; }

        public DbSet<EmployeeCalendarAPI.Models.CalendarEventType> CalendarEventTypes { get; set; }
    }
}
