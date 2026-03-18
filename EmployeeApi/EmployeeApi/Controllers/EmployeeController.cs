using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeDbContext _context;


        public EmployeeController(EmployeeDbContext context)
        {

            _context = context;
        }


       
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees.ToList();
                
            return Ok(employees);

        }
       
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {

         _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
                return NotFound();

         _context.Employees.Remove(emp);
            _context.SaveChanges();
            return Ok();
        }
    }
}