using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeCalendarAPI.Data;
using EmployeeCalendarAPI.Models;
using EmployeeCalendarAPI.Repositories;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace EmployeeCalendarAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]  
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeCalendarAPIContext context)
        {
            employeeRepository = new EmployeeRepository(context);
        }


        /// <summary>
        /// Action to retrieve all employees
        /// </summary>
        /// <returns>Returns a list of all employees or an empty list, if no employee is created yet</returns>
        /// <response code="200">Returned if the list of employees was retrieved</response>
        /// <response code="400">Returned if the list of employees could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return Ok(employeeRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to retrieve one employee by id
        /// </summary>
        /// <returns>Returns employee</returns>
        /// <response code="200">Returned if the employee was retrieved</response>
        /// <response code="400">Returned if the employee could not be retrieved</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            try
            {
                var employee = employeeRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return employee;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update employee
        /// </summary>
        /// <param name="id">Id of employee to update</param>    
        /// <param name="employee">Object to update a new employee</param>      
        /// <response code="200">Returned if the employee was updated</response>
        /// <response code="400">Returned if the employee could not be updated</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/Employees/5        
        [HttpPut("{id}")]
        public ActionResult PutEmployee(int id, [FromBody]Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest();
                }

                employeeRepository.Update(employee);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to crete new employee
        /// </summary>
        /// <param name="employee">Object to create a new employee</param>      
        /// <returns>Returns created employee</returns>
        /// <response code="200">Returned if the employee was created</response>
        /// <response code="400">Returned if the employee could not be created</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Employees
        [HttpPost]
        public ActionResult<Employee> PostEmployee([FromBody]Employee employee)
        {
            try
            {
                employeeRepository.Create(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to delete employee
        /// </summary>
        /// <param name="id">Id of employee to be deleted</param>      
        /// <returns>Returns deleted employee</returns>
        /// <response code="200">Returned if the employee was deleted</response>
        /// <response code="400">Returned if the employee could not be deleted</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            try
            {
                var employee = employeeRepository.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                employeeRepository.Delete(employee);

                return employee;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
