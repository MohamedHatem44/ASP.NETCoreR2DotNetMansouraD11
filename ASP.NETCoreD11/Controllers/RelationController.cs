using ASP.NETCoreD11.Context;
using ASP.NETCoreD11.DTOs;
using ASP.NETCoreD11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public RelationController(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        // Get Employee By Id
        [HttpGet("/EmployeesV01/{id:int}")]
        public ActionResult<Employee> GetByIdV01([FromRoute] int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found" });
            }

            return Ok(employee);
        }
        /*------------------------------------------------------------------*/
        // Get Employee By Id
        [HttpGet("/EmployeesV02/{id:int}")]
        public ActionResult<EmployeeReadDto> GetByIdV02([FromRoute] int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found" });
            }

            var employeeReadDto = new EmployeeReadDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId ?? 0,
                DepartmentName = employee.Department?.Name ?? string.Empty
            };

            return Ok(employeeReadDto);
        }
        /*------------------------------------------------------------------*/
    }
}
