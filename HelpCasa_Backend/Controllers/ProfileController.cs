using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpCasa.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação e registro de usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor da classe AuthController, responsável pela injeção de dependência do contexto de banco de dados.
        /// </summary>
        /// <param name="context">O contexto de banco de dados injetado.</param>
        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint para obter informações do usuário pelo e-mail.
        /// </summary>
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            // Buscar o usuário no banco de dados pelo email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Verifica se o usuário foi encontrado
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Verifica o tipo de usuário (Empregado ou Empregador) e retorna informações específicas
            if (user is Employee employee)
            {
                // Mapear Employee para EmployeeResponseDto
                var response = new EmployeeResponseDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    ProfilePicture = employee.ProfilePicture,
                    Description = employee.Description,
                    Rating = employee.Rating,
                    AvailableTimeRange = employee.AvailableTimeRange,
                    AreaOfExpertise = employee.AreaOfExpertise,
                    Experience = employee.Experience,
                    OfferedServices = employee.OfferedServices?.Select(s => s.ServiceName).ToList(),
                    UserType = "Empregado"
                };

                return Ok(response);
            }
            else if (user is Employer employer)
            {
                // Mapear Employer para EmployerResponseDto
                var response = new EmployerResponseDto
                {
                    Id = employer.Id,
                    Name = employer.Name,
                    Email = employer.Email,
                    Address = employer.Address,
                    Phone = employer.Phone,
                    ProfilePicture = employer.ProfilePicture,
                    Description = employer.Description,
                    Rating = employer.Rating,
                    ContractedServices = employer.ContractedServices?.Select(s => s.ServiceName).ToList(),
                    UserType = "Empregador"
                };

                return Ok(response);
            }

            // Se o tipo de usuário for desconhecido
            return BadRequest(new { message = "Invalid user type" });
        }

        /// <summary>
        /// Endpoint para atualizar o perfil do usuário.
        /// </summary>
        [HttpPut("user/update")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileRequestDto profileData)
        {
            if (profileData == null)
            {
                return BadRequest(new { message = "Invalid data" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == profileData.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Atualizar campos comuns
            user.Name = profileData.Name;
            user.Phone = profileData.Phone;
            user.ProfilePicture = profileData.ProfilePicture;
            user.Description = profileData.Description;
            user.Address = profileData.Address;

            // Atualizar campos específicos de Employee
            if (user is Employee employee)
            {
                employee.AreaOfExpertise = profileData.AreaOfExpertise;
                employee.Experience = profileData.Experience;
                employee.AvailableTimeRange = profileData.AvailableTimeRange;
            }

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the profile", error = ex.Message });
            }
        }
    }
}