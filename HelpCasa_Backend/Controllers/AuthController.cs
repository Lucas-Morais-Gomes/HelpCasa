using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;


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
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Construtor da classe AuthController, responsável pela injeção de dependência do contexto de banco de dados.
    /// </summary>
    /// <param name="context">O contexto de banco de dados injetado.</param>
    /// <param name="emailSender"></param>
    public AuthController(ApplicationDbContext context, IEmailSender emailSender)
    {
      _context = context;
      _emailSender = emailSender;
    }

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    /// <param name="UserDto">Dados do usuário a ser registrado, contendo tipo de usuário, nome, email, senha, endereço e telefone.</param>
    /// <returns>Retorna uma mensagem de sucesso ou erro, caso o email já esteja registrado.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto UserDto)
    {
      var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserDto.Email);
      if (existingUser != null)
      {
        return Conflict(new { message = "Email already registered." });
      }

      User User;

      if (UserDto.UserType == "employee")
      {
        User = new Employee
        {
          Name = UserDto.Name,
          Email = UserDto.Email,
          Password = BCrypt.Net.BCrypt.HashPassword(UserDto.Password),
          Address = UserDto.Address,
          Phone = UserDto.Phone,
          AreaOfExpertise = UserDto.AreaOfExpertise,
          Experience = UserDto.Experience
        };
      }
      else
      {
        User = new Employer
        {
          Name = UserDto.Name,
          Email = UserDto.Email,
          Password = BCrypt.Net.BCrypt.HashPassword(UserDto.Password),
          Address = UserDto.Address,
          Phone = UserDto.Phone,
        };
      }

      _context.Users.Add(User);
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
      var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email);
      if (dbUser == null || !BCrypt.Net.BCrypt.Verify(usuarioLogin.Password, dbUser.Password))
      {
        return Unauthorized(new { message = "Invalid email or password" });
      }

      return Ok(new { message = "Login successful" });
    }

    /// <summary>
    /// Envia um email para o usuário caso ele esqueça a sua senha
    /// </summary>
    /// <param name="forgotPasswordDto"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
      if (user == null)
      {
        return BadRequest(new { message = "User not found" });
      }

      // Gerar um token para redefinir a senha
      var resetToken = Guid.NewGuid().ToString();
      user.ResetPasswordToken = resetToken;

      // Aplique a lógica de conversão para UTC
      var myDateTime = DateTime.Now.ToUniversalTime();
      if (myDateTime.Kind == DateTimeKind.Local)
      {
        myDateTime = myDateTime.ToUniversalTime();
      }
      user.ResetPasswordExpire = myDateTime.AddHours(1); // O token expira em 1 hora

      _context.Users.Update(user);
      await _context.SaveChangesAsync();

      // Envio de e-mail de redefinição
      var resetLink = $"http://localhost:3000/forgot-password-solicitation?token={resetToken}";

      await _emailSender.SendEmailAsync(
        user.Email,
        "Redefinição de Senha",
        $"Clique no link abaixo para redefinir sua senha:\n\n{resetLink}"
      );


      return Ok(new { message = "Link de redefinição de senha enviado para o e-mail." });
    }


    // Método para atualizar a senha
    [HttpPut("reset-password/{token}")]
    public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPasswordDto resetPasswordDto)
    {
      var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetPasswordToken == token);
      if (user == null || user.ResetPasswordExpire < DateTime.Now)
      {
        return BadRequest(new { message = "Token inválido ou expirado." });
      }

      user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
      user.ResetPasswordToken = null;
      user.ResetPasswordExpire = null;
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
      Console.WriteLine("Senha redefinida com sucesso");
      return Ok(new { message = "Senha redefinida com sucesso." });
    }
  }
}
