using ASP.NETCoreD11.Context;
using ASP.NETCoreD11.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        // Get All Employees
        // IActionResult => Not Reccommended
        // ActionResult<List<Employee>> => Reccommended
        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }
        /*------------------------------------------------------------------*/
        // Get Employee By Id
        [HttpGet("{id:int}")]
        // Get: api/employees/1
        // Get: api/employees?id=1 XXXXX
        public ActionResult<Employee> GetById([FromRoute] int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found" });
            }
            return Ok(employee);
        }
        /*------------------------------------------------------------------*/
        // Get Employee By Name
        [HttpGet("{name:alpha}")]
        public ActionResult<Employee> GetByName([FromRoute] string name)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Name == name);
            if (employee == null)
            {
                return NotFound(new { Message = $"Employee with name {name} not found" });
            }
            return Ok(employee);
        }
        /*------------------------------------------------------------------*/
        // Create Employee V01
        [HttpPost]
        [Route("CreateV01")]
        public ActionResult<Employee> CreateV01([FromBody] Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Add(employee);
            _context.SaveChanges();
            return Ok(employee); // 200 OK
        }
        /*------------------------------------------------------------------*/
        // Create Employee V02
        [HttpPost]
        [Route("CreateV02")]
        public ActionResult CreateV02([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Add(employee);
            _context.SaveChanges();
            return Created(); // 201 Created
        }
        /*------------------------------------------------------------------*/
        // Create Employee V03
        [HttpPost]
        [Route("CreateV03")]
        public ActionResult<Employee> CreateV03([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Add(employee);
            _context.SaveChanges();
            return Created($"/api/employees/{employee.Id}", employee); // 201 Created
        }
        /*------------------------------------------------------------------*/
        // Create Employee V04
        [HttpPost]
        [Route("CreateV04")]
        public ActionResult<Employee> CreateV04([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Add(employee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }
        /*------------------------------------------------------------------*/
        // Update Employee
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Update(int id,[FromBody] Employee employee)
        {
            if(id != employee.Id)
            {
                return BadRequest(new { Message = $"Employee id in route {id} does not match employee id in body {employee.Id}" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeFromDb = _context.Employees.Find(id);
            if (employeeFromDb == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found" });
            }

            employeeFromDb.Name = employee.Name;
            employeeFromDb.Age = employee.Age;
            employeeFromDb.Salary = employee.Salary;

            _context.SaveChanges();
            return Ok(employeeFromDb);
        }
        /*------------------------------------------------------------------*/
        // Delete Employee
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult Delete(int id)
        {
            var employeeFromDb = _context.Employees.Find(id);
            if (employeeFromDb == null)
            {
                return NotFound(new { Message = $"Employee with id {id} not found" });
            }

            _context.Employees.Remove(employeeFromDb);
            _context.SaveChanges();
            return NoContent(); // 204 No Content
        }
        /*------------------------------------------------------------------*/
    }
}
