using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HelpCasa.Models
{
  public abstract class User
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório")]
    public string Address { get; set; }

    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage = "Telefone é obrigatório")]
    public string Phone { get; set; }

    public int Rating { get; set; }

    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordExpire { get; set; }

    public required bool Subscription { get; set; }
  }

  public class Employee : User
  {
    public string? AvailableTimeRange { get; set; }
    public string? AreaOfExpertise { get; set; }

    [JsonIgnore]
    public List<Service>? OfferedServices { get; set; }
    public string? Experience { get; set; }
  }

  public class Employer : User
  {
    [JsonIgnore]
    public List<Service>? ContractedServices { get; set; }
  }
}
