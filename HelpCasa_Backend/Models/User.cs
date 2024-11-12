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

    // Propriedades para redefinir a senha
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordExpire { get; set; }
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
    public string? Experience { get; set; }
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
}