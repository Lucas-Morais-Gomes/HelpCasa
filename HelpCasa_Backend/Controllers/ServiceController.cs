using HelpCasa.Models;
using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpCasa.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria um novo serviço.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Valida se o empregado e o empregador existem
            var employee = await _context.Users.FindAsync(serviceDto.EmployeeId);
            if (employee == null)
                return NotFound(new { message = "Empregado não encontrado." });

            var employer = await _context.Users.FindAsync(serviceDto.EmployerId);
            if (employer == null)
                return NotFound(new { message = "Empregador não encontrado." });

            // Cria o novo serviço
            var service = new Service
            {
                ServiceName = serviceDto.ServiceName,
                ServiceDescription = serviceDto.ServiceDescription,
                ServicePrice = serviceDto.ServicePrice,
                Location = serviceDto.Location,
                DateTime = serviceDto.DateTime,
                Employee = (Employee)employee,
                Employer = (Employer)employer
            };

            // Adiciona ao banco de dados
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        /// <summary>
        /// Obtém um serviço pelo ID (para uso com CreatedAtAction).
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _context.Services
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
                return NotFound();

            return Ok(service);
        }
    }
}
