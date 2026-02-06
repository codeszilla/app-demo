using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using MyApp.Server.Data;
using MyApp.Server.Models;


namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        private List<Department> departments = new();
        private string newDepartmentName = string.Empty;
        private int newDepartmentStatus = 0; // default as inactive

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
           // return await _context.Departments.ToListAsync();

            AppDbContext Db = new AppDbContext();
            var departments = await _context.Departments.FromSqlRaw("EXEC GetDepartments").ToListAsync();
            //* via SP  :D
            return departments;

      
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return department;
        }

  
        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = department.DId }, department);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Department department)
        {
            if (id != department.DId) return BadRequest();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
  
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
