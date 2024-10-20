using System.ComponentModel.DataAnnotations;

namespace HelpCasa.Models
{
  public abstract class User
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Address { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public required string Phone { get; set; }
    public int Rating { get; set; }
  }

  public class Employee : User
  {
    public string? AvailableTimeRange { get; set; }
    public string? AreaOfExpertise { get; set; }
    public List<Service>? OfferedServices { get; set; }
    public List<string>? Experience { get; set; }
  }

  public class Employer : User
  {
    public List<Service>? ContractedServices { get; set; }
  }

  public class UserDto
  {
    [Required(ErrorMessage = "name é obrigatório.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "email é obrigatório.")]
    [EmailAddress(ErrorMessage = "email inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    public required string PasswordDto { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório.")]
    public required string Address { get; set; }

    [Required(ErrorMessage = "phone é obrigatório.")]
    public required string Phone { get; set; }

    [Required(ErrorMessage = "Tipo de usuário é obrigatório.")]
    public required string UserType { get; set; } // "Empregado" ou "Empregador"
  }


  public class UsuarioLoginDto
  {
    public required string Email { get; set; }
    public required string PasswordDto { get; set; }
  }


}
