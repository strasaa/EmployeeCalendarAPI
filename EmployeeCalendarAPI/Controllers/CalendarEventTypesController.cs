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
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventTypesController : ControllerBase
    {
        private readonly CalendarEventTypeRepository calendarEventTypeRepository;

        public CalendarEventTypesController(EmployeeCalendarAPIContext context)
        {
            calendarEventTypeRepository = new CalendarEventTypeRepository(context);
        }

        /// <summary>
        /// Action to retrieve all calendar event types
        /// </summary>
        /// <returns>Returns a list of all calendar event types or an empty list, if no calendar event type is created yet</returns>
        /// <response code="200">Returned if the list of calendar event types was retrieved</response>
        /// <response code="400">Returned if the list of calendar event types could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEventTypes
        [HttpGet]
        public ActionResult<IEnumerable<CalendarEventType>> GetCalendarEventTypeTypes()
        {
            try
            {
                return Ok(calendarEventTypeRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to retrieve one calendar event type by id
        /// </summary>
        /// <returns>Returns calendar event type</returns>
        /// <response code="200">Returned if the calendar event type was retrieved</response>
        /// <response code="400">Returned if the calendar event type could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/CalendarEventTypes/5
        [HttpGet("{id}")]
        public ActionResult<CalendarEventType> GetCalendarEventTypeById(int id)
        {
            try
            {
                var calendarEventType = calendarEventTypeRepository.GetById(id);

                if (calendarEventType == null)
                {
                    return NotFound();
                }

                return calendarEventType;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update calendar event type
        /// </summary>
        /// <param name="id">Id of calendar event type to update</param>    
        /// <param name="calendarEventType">Object to update a new calendar event type</param>      
        /// <response code="200">Returned if the calendar event type was updated</response>
        /// <response code="400">Returned if the calendar event type could not be updated</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/CalendarEventTypes/5        
        [HttpPut("{id}")]
        public ActionResult PutCalendarEventType(int id, [FromBody]CalendarEventType calendarEventType)
        {
            try
            {
                if (id != calendarEventType.Id)
                {
                    return BadRequest();
                }

                calendarEventTypeRepository.Update(calendarEventType);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to crete new calendar event type
        /// </summary>
        /// <param name="calendarEventType">Object to create a new calendar event type</param>      
        /// <returns>Returns created calendar event type</returns>
        /// <response code="200">Returned if the calendar event type was created</response>
        /// <response code="400">Returned if the calendar event type could not be created</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/CalendarEventTypes
        [HttpPost]
        public ActionResult<CalendarEventType> PostCalendarEventType([FromBody]CalendarEventType calendarEventType)
        {
            try
            {
                calendarEventTypeRepository.Create(calendarEventType);

                return Ok(calendarEventType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to delete calendar event type
        /// </summary>
        /// <param name="id">Id of calendar event type to be deleted</param>      
        /// <returns>Returns deleted calendar event type</returns>
        /// <response code="200">Returned if the calendar event type was deleted</response>
        /// <response code="400">Returned if the calendar event type could not be deleted</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/CalendarEventTypes/5
        [HttpDelete("{id}")]
        public ActionResult<CalendarEventType> DeleteCalendarEventType(int id)
        {
            try
            {
                var calendarEventType = calendarEventTypeRepository.GetById(id);
                if (calendarEventType == null)
                {
                    return NotFound();
                }

                calendarEventTypeRepository.Delete(calendarEventType);

                return calendarEventType;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
