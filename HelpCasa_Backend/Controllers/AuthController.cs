using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace HelpCasa.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto UserDto) // Crie um DTO para registrar
    {
      // Verifique o tipo de usuário (empregado ou empregador) e crie a instância correspondente
      User User;

      if (UserDto.UserType == "Empregado")
      {
        User = new Employee
        {
          Name = UserDto.Name,
          Email = UserDto.Email,
          Password = BCrypt.Net.BCrypt.HashPassword(UserDto.PasswordDto),
          Address = UserDto.Address,
          Phone = UserDto.Phone,
        };
      }
      else // Assumindo que o outro tipo é "Empregador"
      {
        User = new Employer
        {
          Name = UserDto.Name,
          Email = UserDto.Email,
          Password = BCrypt.Net.BCrypt.HashPassword(UserDto.PasswordDto),
          Address = UserDto.Address,
          Phone = UserDto.Phone,
        };
      }

      _context.Usuarios.Add(User); // Assumindo que você tem uma DbSet<User> no ApplicationDbContext
      await _context.SaveChangesAsync();

      return Ok(new { message = "User created successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLogin)
    {
      var dbUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email);
      if (dbUser == null || !BCrypt.Net.BCrypt.Verify(usuarioLogin.PasswordDto, dbUser.Password))
      {
        return Unauthorized(new { message = "Invalid email or password" });
      }

      return Ok(new { message = "Login successful" });
    }

  }
}
