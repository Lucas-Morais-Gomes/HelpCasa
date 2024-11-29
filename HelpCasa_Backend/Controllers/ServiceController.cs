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
        /// Cria um novo serviço (somente o empregador pode criar).
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceDto serviceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Valida se o empregador existe
            var user = await _context.Users.FindAsync(serviceDto.EmployerId);
            if (user == null)
                return NotFound(new { message = "Empregador não encontrado." });

            // Verifica se o usuário é do tipo Empregador
            var employer = user as Employer;
            if (employer == null)
                return BadRequest(new { message = "Usuário não é um empregador." });

            // Cria o novo serviço
            var service = new Service
            {
                ServiceName = serviceDto.ServiceName,
                ServiceDescription = serviceDto.ServiceDescription,
                ServicePrice = serviceDto.ServicePrice,
                Location = serviceDto.Location,
                Category = serviceDto.Category,
                DateTime = serviceDto.DateTime.ToUniversalTime(), // Garantir que a data esteja em UTC
                Employer = employer,
                IsActive = true
            };

            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao salvar o serviço", details = ex.Message });
            }

            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }


        /// <summary>
        /// O empregado aceita um serviço, associando-se a ele e desativando o serviço.
        /// </summary>
        [HttpPost("accept/{serviceId}")]
        public async Task<IActionResult> AcceptService(int serviceId, [FromBody] int employeeId)
        {
            // Valida se o empregado existe
            var employee = await _context.Users.FindAsync(employeeId);
            if (employee == null)
                return NotFound(new { message = "Empregado não encontrado ou usuário não é empregado." });

            // Valida se o serviço existe
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId);
            if (service == null)
                return NotFound(new { message = "Serviço não encontrado." });

            // Verifica se o serviço já foi aceito
            if (service.Employee != null)
            {
                return BadRequest(new { message = "Este serviço já foi aceito por outro empregado." });
            }

            // Associa o empregado ao serviço
            service.Employee = (Employee)employee;

            // Desativa o serviço, pois foi aceito
            service.IsActive = false;

            try
            {
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao salvar a aceitação do serviço", details = ex.Message });
            }

            return Ok(new { message = "Serviço aceito com sucesso.", service });
        }



        /// <summary>
        /// Obtém um serviço pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _context.Services
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);  // Filtra apenas serviços ativos

            if (service == null)
                return NotFound();

            return Ok(service);
        }


        /// <summary>
        /// Obtém todos os serviços cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _context.Services
                .Where(s => s.IsActive)  // Filtra apenas serviços ativos
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .ToListAsync();

            return Ok(services);
        }


        /// <summary>
        /// Obtém serviços por categoria.
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetServicesByCategory(string category)
        {
            var services = await _context.Services
                .Where(s => s.IsActive && s.Category.Contains(category))  // Filtra apenas serviços ativos e pela categoria
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .ToListAsync();

            return Ok(services);
        }


        /// <summary>
        /// Obtém serviços pelo nome.
        /// </summary>
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetServicesByName(string name)
        {
            var services = await _context.Services
                .Where(s => s.IsActive && s.ServiceName.Contains(name))  // Filtra apenas serviços ativos e pelo nome
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .ToListAsync();

            return Ok(services);
        }


        /// <summary>
        /// Obtém serviços pelo preço.
        /// </summary>
        [HttpGet("price/{price}")]
        public async Task<IActionResult> GetServicesByPrice(decimal price)
        {
            var services = await _context.Services
                .Where(s => s.IsActive && s.ServicePrice == price)  // Filtra apenas serviços ativos e pelo preço
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .ToListAsync();

            return Ok(services);
        }


        /// <summary>
        /// Obtém serviços pelo autor (empregador).
        /// </summary>
        [HttpGet("author/{employerId}")]
        public async Task<IActionResult> GetServicesByAuthor(int employerId)
        {
            var services = await _context.Services
                .Where(s => s.IsActive && s.Employer.Id == employerId)  // Filtra apenas serviços ativos e pelo empregador
                .Include(s => s.Employee)
                .Include(s => s.Employer)
                .ToListAsync();

            return Ok(services);
        }

    }
}
