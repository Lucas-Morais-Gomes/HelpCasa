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
    public async Task<IActionResult> Register([FromBody] UsuarioDto usuarioDto) // Crie um DTO para registrar
    {
      // Verifique o tipo de usuário (empregado ou empregador) e crie a instância correspondente
      Usuario usuario;

      if (usuarioDto.TipoUsuario == "Empregado")
      {
        usuario = new Empregado
        {
          Nome = usuarioDto.Nome,
          Email = usuarioDto.Email,
          SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha),
          Endereco = usuarioDto.Endereco,
          Telefone = usuarioDto.Telefone,
          // Defina outras propriedades conforme necessário
        };
      }
      else // Assumindo que o outro tipo é "Empregador"
      {
        usuario = new Empregador
        {
          Nome = usuarioDto.Nome,
          Email = usuarioDto.Email,
          SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Senha),
          Endereco = usuarioDto.Endereco,
          Telefone = usuarioDto.Telefone,
          // Defina outras propriedades conforme necessário
        };
      }

      _context.Usuarios.Add(usuario); // Assumindo que você tem uma DbSet<Usuario> no ApplicationDbContext
      await _context.SaveChangesAsync();

      return Ok(new { message = "User created successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLogin)
    {
      var dbUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email);
      if (dbUser == null || !BCrypt.Net.BCrypt.Verify(usuarioLogin.Senha, dbUser.SenhaHash))
      {
        return Unauthorized(new { message = "Invalid email or password" });
      }

      return Ok(new { message = "Login successful" });
    }

  }
}
