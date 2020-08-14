using EmployeeCalendarAPI.Models;
using System;
using System.Collections.Generic;

namespace EmployeeCalendarAPI.Interfaces
{
    interface ICalendarEvent
    {
        IEnumerable<CalendarEvent> GetAllCalendarEventsByEmployeeId(int id);

        IEnumerable<CalendarEvent> GetCalendarEventsInInterval(DateTime start, DateTime end);

        IEnumerable<CalendarEvent> GetCalendarEventsInIntervalByEmployeeId(DateTime start, DateTime end, int id);
    }
}
