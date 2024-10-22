using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace HelpCasa.Controllers
{
  /// <summary>
  /// Controller responsável pela autenticação e registro de usuários.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Construtor da classe AuthController, responsável pela injeção de dependência do contexto de banco de dados.
    /// </summary>
    /// <param name="context">O contexto de banco de dados injetado.</param>
    public AuthController(ApplicationDbContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    /// <param name="UserDto">Dados do usuário a ser registrado, contendo tipo de usuário, nome, email, senha, endereço e telefone.</param>
    /// <returns>Retorna uma mensagem de sucesso ou erro, caso o email já esteja registrado.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto UserDto)
    {
      // Verifique se o email já está cadastrado
      var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == UserDto.Email);
      if (existingUser != null)
      {
        return BadRequest(new { message = "Email already registered." });
      }

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
          AreaOfExpertise = UserDto.AreaOfExpertise,
          Experience = UserDto.Experience
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

    /// <summary>
    /// Realiza o login de um usuário.
    /// </summary>
    /// <param name="usuarioLogin">Dados de login, contendo email e senha.</param>
    /// <returns>Retorna uma mensagem de sucesso ou erro, caso as credenciais estejam incorretas.</returns>
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
