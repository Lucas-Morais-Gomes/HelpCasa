using System.ComponentModel.DataAnnotations;

namespace HelpCasa.Models
{
  /// <summary>
  /// Classe abstrata que representa um usuário genérico (empregado ou empregador) no sistema.
  /// </summary>
  public abstract class User
  {
    /// <summary>
    /// Identificador único do usuário.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Endereço de email do usuário.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Senha criptografada do usuário.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Endereço residencial do usuário.
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Caminho ou URL da foto de perfil do usuário (opcional).
    /// </summary>
    public string? ProfilePicture { get; set; }

    /// <summary>
    /// Descrição adicional do usuário (opcional).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Número de telefone de contato do usuário.
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Avaliação geral do usuário baseada em feedbacks (valor padrão 0).
    /// </summary>
    public int Rating { get; set; }
  }

  /// <summary>
  /// Classe que representa um empregado no sistema.
  /// </summary>
  public class Employee : User
  {
    /// <summary>
    /// Faixa de horário disponível para trabalhar (opcional).
    /// </summary>
    public string? AvailableTimeRange { get; set; }

    /// <summary>
    /// Área de especialização do empregado (opcional).
    /// </summary>
    public string? AreaOfExpertise { get; set; }

    /// <summary>
    /// Lista de serviços oferecidos pelo empregado (opcional).
    /// </summary>
    public List<Service>? OfferedServices { get; set; }

    /// <summary>
    /// Lista de experiências profissionais anteriores do empregado (opcional).
    /// </summary>
    public List<string>? Experience { get; set; }
  }

  /// <summary>
  /// Classe que representa um empregador no sistema.
  /// </summary>
  public class Employer : User
  {
    /// <summary>
    /// Lista de serviços contratados pelo empregador (opcional).
    /// </summary>
    public List<Service>? ContractedServices { get; set; }
  }

  /// <summary>
  /// Data Transfer Object (DTO) utilizado para o registro de novos usuários.
  /// </summary>
  public class UserDto
  {
    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    [Required(ErrorMessage = "name é obrigatório.")]
    public required string Name { get; set; }

    /// <summary>
    /// Endereço de email do usuário.
    /// </summary>
    [Required(ErrorMessage = "email é obrigatório.")]
    [EmailAddress(ErrorMessage = "email inválido.")]
    public required string Email { get; set; }

    /// <summary>
    /// Senha para o cadastro do usuário.
    /// </summary>
    [Required(ErrorMessage = "Senha é obrigatória.")]
    public required string PasswordDto { get; set; }

    /// <summary>
    /// Endereço residencial do usuário.
    /// </summary>
    [Required(ErrorMessage = "Endereço é obrigatório.")]
    public required string Address { get; set; }

    /// <summary>
    /// Número de telefone de contato do usuário.
    /// </summary>
    [Required(ErrorMessage = "phone é obrigatório.")]
    public required string Phone { get; set; }

    /// <summary>
    /// Tipo de usuário: "Empregado" ou "Empregador".
    /// </summary>
    [Required(ErrorMessage = "Tipo de usuário é obrigatório.")]
    public required string UserType { get; set; }
  }

  /// <summary>
  /// DTO utilizado para o login de usuários no sistema.
  /// </summary>
  public class UsuarioLoginDto
  {
    /// <summary>
    /// Endereço de email utilizado para login.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Senha do usuário utilizada para login.
    /// </summary>
    public required string PasswordDto { get; set; }
  }
}
