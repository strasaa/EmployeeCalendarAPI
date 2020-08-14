using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeCalendarAPI.Data;
using EmployeeCalendarAPI.Models;
using EmployeeCalendarAPI.Repositories;
using System;
using Microsoft.AspNetCore.Http;

namespace EmployeeCalendarAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly CalendarEventRepository calendarEventRepository;
        private readonly EmployeeRepository employeeRepository;

        public CalendarEventsController(EmployeeCalendarAPIContext context)
        {
            calendarEventRepository = new CalendarEventRepository(context);
            employeeRepository = new EmployeeRepository(context);
        }

        /// <summary>
        /// Action to retrieve all calendar events
        /// </summary>
        /// <returns>Returns a list of all calendar events or an empty list, if no calendar event is created yet</returns>
        /// <response code="200">Returned if the list of calendar events was retrieved</response>
        /// <response code="400">Returned if the list of calendar events could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEvents/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public ActionResult<IEnumerable<CalendarEvent>> GetCalendarEvents()
        {
            try
            {
                return Ok(calendarEventRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to retrieve one calendar event by id
        /// </summary>
        /// <returns>Returns calendar event</returns>
        /// <response code="200">Returned if the calendar event was retrieved</response>
        /// <response code="400">Returned if the calendar event could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEvents/GetById/5
        [HttpGet("{id}")]
        [ActionName("GetById")]
        public ActionResult<CalendarEvent> GetCalendarEventById(int id)
        {
            try
            {
                var calendarEvent = calendarEventRepository.GetById(id);

                if (calendarEvent == null)
                {
                    return NotFound();
                }

                return calendarEvent;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to get all calendar events for given employee 
        /// </summary>            
        /// <param name="id">Id of employee</param>      
        /// <returns>Returns a list of all calendar events or an empty list, if there is no event for given employee</returns>
        /// <response code="200">Returned if the list of calendar events was retrieved</response>
        /// <response code="400">Returned if the list of calendar events could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEvents/GetByEmployeeId/5
        [HttpGet("{id}")]
        [ActionName("GetByEmployeeId")]
        public ActionResult<IEnumerable<CalendarEvent>> GetAllCalendarEventsByEmployeeId(int id)
        {
            try
            {
                var employee = employeeRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(calendarEventRepository.GetAllCalendarEventsByEmployeeId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to get all calendar events between two dates in ISO formats
        /// </summary>
        /// <param name="start">Start date time of the interval in ISO format / 2020-8-11T04:20:00</param>        
        /// <param name="end">End date time of the interval in ISO format / 2020-8-11T04:20:00</param>    
        /// <returns>Returns a list of all calendar events or an empty list, if no calendar event is matching requirements</returns>
        /// <response code="200">Returned if the list of calendar events was retrieved</response>
        /// <response code="400">Returned if the list of calendar events could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEvents/GetAllBetween/2020-8-11T04:20:00/2020-8-15T14:13:00
        [HttpGet("{start}/{end}")]
        [ActionName("GetAllBetween")]
        public ActionResult<IEnumerable<CalendarEvent>> GetAllCalendarEventsInInterval(string start, string end)
        {
            try
            {
                var startTime = DateTime.Parse(start);
                var endTime = DateTime.Parse(end);
                return Ok(calendarEventRepository.GetCalendarEventsInInterval(startTime, endTime));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to get all calendar events between two dates in ISO formats for given employee 
        /// </summary>
        /// <param name="start">Start date time of the interval in ISO format / 2020-8-11T04:20:00</param>        
        /// <param name="end">End date time of the interval in ISO format / 2020-8-11T04:20:00</param>         
        /// <param name="id">Id of employee</param>      
        /// <returns>Returns a list of all calendar events or an empty list, if no calendar event is matching requirements</returns>
        /// <response code="200">Returned if the list of calendar events was retrieved</response>
        /// <response code="400">Returned if the list of calendar events could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEvents/GetAllBetween/2020-8-11T04:20:00/2020-8-15T14:13:00/3 
        [HttpGet("{start}/{end}/{id}")]
        [ActionName("GetAllBetweenByEmployeeId")]
        public ActionResult<IEnumerable<CalendarEvent>> GetAllCalendarEventsInIntervalByEmployeeId(string start, string end, int id)
        {
            try
            {
                var startTime = DateTime.Parse(start);
                var endTime = DateTime.Parse(end);
                return Ok(calendarEventRepository.GetCalendarEventsInIntervalByEmployeeId(startTime, endTime, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update calendar event 
        /// </summary>
        /// <param name="id">Id of calendar event to update</param>    
        /// <param name="calendarEvent">Object to update a new calendar event</param>      
        /// <response code="200">Returned if the calendar event was updated</response>
        /// <response code="400">Returned if the calendar event could not be updated</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/CalendarEvents/Put/5
        [HttpPut("{id}")]
        [ActionName("Put")]
        public ActionResult PutCalendarEvent(int id, [FromBody]CalendarEvent calendarEvent)
        {
            try
            {
                if (id != calendarEvent.Id)
                {
                    return BadRequest();
                }

                calendarEventRepository.Update(calendarEvent);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to crete new calendar event 
        /// </summary>
        /// <param name="calendarEvent">Object to create a new calendar event </param>      
        /// <returns>Returns created calendar event</returns>
        /// <response code="200">Returned if the calendar event was created</response>
        /// <response code="400">Returned if the calendar event could not be created</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/CalendarEvents/Post      
        [HttpPost]
        [ActionName("Post")]
        public ActionResult<CalendarEvent> PostCalendarEvent([FromBody]CalendarEvent calendarEvent)
        {
            try
            {
                calendarEventRepository.Create(calendarEvent);

                return Ok(calendarEvent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to delete calendar event 
        /// </summary>
        /// <param name="id">Id of calendar event to be deleted</param>      
        /// <returns>Returns deleted calendar event</returns>
        /// <response code="200">Returned if the calendar event was deleted</response>
        /// <response code="400">Returned if the calendar event could not be deleted</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/CalendarEvents/Delete/5
        [HttpDelete("{id}")]
        [ActionName("Delete")]
        public ActionResult<CalendarEvent> DeleteCalendarEvent(int id)
        {
            try
            {
                var calendarEvent = calendarEventRepository.GetById(id);
                if (calendarEvent == null)
                {
                    return NotFound();
                }

                calendarEventRepository.Delete(calendarEvent);

                return calendarEvent;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
