using EmployeeCalendarAPI.Data;
using EmployeeCalendarAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCalendarAPI.Repositories
{
    public class CalendarEventTypeRepository
    {
        protected readonly EmployeeCalendarAPIContext _context;

        public CalendarEventTypeRepository(EmployeeCalendarAPIContext context)
        {
            _context = context;
        }

        public void Create(CalendarEventType calendarEventType)
        {
            _context.CalendarEventTypes.Add(calendarEventType);
            _context.SaveChanges();
        }

        public void Delete(CalendarEventType calendarEventType)
        {
            _context.CalendarEventTypes.Remove(calendarEventType);
            _context.SaveChanges();
        }

        public IEnumerable<CalendarEventType> GetAll()
        {
            return _context.CalendarEventTypes.ToList();
        }

        public CalendarEventType GetById(int id)
        {
            return _context.CalendarEventTypes.Find(id);
        }

        public void Update(CalendarEventType calendarEventType)
        {
            _context.CalendarEventTypes.Attach(calendarEventType);
            _context.Entry(calendarEventType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
