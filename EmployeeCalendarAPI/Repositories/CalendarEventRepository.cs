using EmployeeCalendarAPI.Data;
using EmployeeCalendarAPI.Interfaces;
using EmployeeCalendarAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCalendarAPI.Repositories
{
    public class CalendarEventRepository : IRepository<CalendarEvent>, ICalendarEvent
    {
        protected readonly EmployeeCalendarAPIContext _context;

        public CalendarEventRepository(EmployeeCalendarAPIContext context)
        {
            _context = context;
        }

        public void Create(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Add(calendarEvent);
            _context.SaveChanges();
        }

        public void Delete(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Remove(calendarEvent);
            _context.SaveChanges();
        }

        public IEnumerable<CalendarEvent> GetAll()
        {
            return _context.CalendarEvents.ToList();
        }

        public CalendarEvent GetById(int id)
        {
            return _context.CalendarEvents.Find(id);
        }

        public void Update(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Attach(calendarEvent);
            _context.Entry(calendarEvent).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<CalendarEvent> GetAllCalendarEventsByEmployeeId(int id)
        {
            var result = _context.CalendarEvents.Where(calendarEvent => calendarEvent.EmployeeId == id).ToList();
            return result;
        }

        public IEnumerable<CalendarEvent> GetCalendarEventsInInterval(DateTime start, DateTime end)
        {
            return _context.CalendarEvents.Where(calendarEvent =>
            // Calendar event starts in interval
            (calendarEvent.StartTime >= start && calendarEvent.StartTime < end) ||
            // Calendar event ends in interval
            (calendarEvent.EndTime > start && calendarEvent.EndTime <= end) ||
            // Calendar event is over whole interval
            (calendarEvent.StartTime < start && calendarEvent.EndTime >= end)).ToList();
        }

        public IEnumerable<CalendarEvent> GetCalendarEventsInIntervalByEmployeeId(DateTime start, DateTime end, int id)
        {
            return _context.CalendarEvents.Where(calendarEvent => calendarEvent.EmployeeId == id &&
            // Calendar event starts in interval
            ((calendarEvent.StartTime >= start && calendarEvent.StartTime < end) ||
            // Calendar event ends in interval
            (calendarEvent.EndTime > start && calendarEvent.EndTime <= end) ||
            // Calendar event is over whole interval
            (calendarEvent.StartTime < start && calendarEvent.EndTime >= end))).ToList();
        }
    }
}
