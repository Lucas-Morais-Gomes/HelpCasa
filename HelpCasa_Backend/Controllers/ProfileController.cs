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
        [HttpGet("user/email/{email}")]
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
                    UserType = "Empregado",
                    Subscription = employee.Subscription
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
                    UserType = "Empregador",
                    Subscription = employer.Subscription
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

        [HttpPost("user/rate/{ratedUserId}")]
        public async Task<IActionResult> RateUser(int ratedUserId, [FromBody] RatingRequestDto ratingRequest)
        {
            if (ratingRequest == null || ratingRequest.RatingValue < 1 || ratingRequest.RatingValue > 5)
            {
                return BadRequest(new { message = "Invalid rating value. It must be between 1 and 5." });
            }

            var ratingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == ratingRequest.RatingUserEmail);
            if (ratingUser == null)
            {
                return BadRequest(new { message = "Rating user not found" });
            }

            var ratedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == ratedUserId);
            if (ratedUser == null)
            {
                return NotFound(new { message = "User to be rated not found" });
            }

            // Verifica se o usuário que está sendo avaliado é diferente do usuário que está avaliando
            if (ratingUser.Id == ratedUser.Id)
            {
                return BadRequest(new { message = "You cannot rate yourself" });
            }

            // Atualiza os dados de avaliação
            ratedUser.TotalRating += ratingRequest.RatingValue;
            ratedUser.NumberOfRatings += 1;
            ratedUser.AverageRating = (double)ratedUser.TotalRating / ratedUser.NumberOfRatings;

            _context.Users.Update(ratedUser);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Rating submitted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the rating", error = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint para obter a avaliação de um usuário específico pelo seu ID.
        /// </summary>
        [HttpGet("user/{userId}/rating")]
        public async Task<IActionResult> GetUserRating(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Retorna as informações de avaliação do usuário
            var ratingInfo = new
            {
                TotalRating = user.TotalRating,
                NumberOfRatings = user.NumberOfRatings,
                AverageRating = user.AverageRating
            };

            return Ok(ratingInfo);
        }

        /// <summary>
        /// Endpoint para obter informações do usuário pelo ID.
        /// </summary>
        [HttpGet("user/id/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            // Buscar o usuário no banco de dados pelo ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

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
                    UserType = "Empregado",
                    Subscription = employee.Subscription
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
                    UserType = "Empregador",
                    Subscription = employer.Subscription
                };

                return Ok(response);
            }

            // Se o tipo de usuário for desconhecido
            return BadRequest(new { message = "Invalid user type" });
        }

    }
}