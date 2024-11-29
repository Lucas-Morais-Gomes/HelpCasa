using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using HelpCasa.Services;
using System.Text;

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
          Experience = UserDto.Experience,
          Subscription = false
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
          Subscription = false
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

      // Verifica o tipo de usuário (Empregado ou Empregador) e retorna informações específicas
      if (dbUser is Employee employee)
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
      else if (dbUser is Employer employer)
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
    /// Envia um email para o usuário caso ele esqueça a sua senha
    /// </summary>
    /// <param name="forgotPasswordDto"></param>
    /// <param name="mailgunOptions"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto, [FromServices] IOptions<MailgunSettings> mailgunOptions)
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

      // Configuração do Mailgun
      var mailgunSettings = mailgunOptions.Value;
      var resetLink = $"http://localhost:3000/forgot-password-solicitation?token={resetToken}";

      // Enviar o e-mail usando o MailgunEmailSender
      var subject = "Redefinição de Senha";
      var body = $"Clique no link abaixo para redefinir sua senha:\n\n{resetLink}";

      var response = MailgunEmailSender.SendEmail(user.Email, subject, body);

      // Verifique a resposta do envio de e-mail
      if (response.IsSuccessful)
      {
        return Ok(new { message = "Link de redefinição de senha enviado para o e-mail." });
      }
      else
      {
        var errorResponse = response.Content;
        Console.WriteLine($"Mailgun error response: {errorResponse}");
        return StatusCode(500, new { message = "Failed to send reset password email.", details = errorResponse });
      }
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
