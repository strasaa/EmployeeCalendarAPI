using EmployeeCalendarAPI.Data;
using EmployeeCalendarAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCalendarAPI.Repositories
{
    public class EmployeeRepository
    {
        protected readonly EmployeeCalendarAPIContext _context;

        public EmployeeRepository(EmployeeCalendarAPIContext context)
        {
            _context = context;
        }

        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Attach(employee);
            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
