using System.ComponentModel.DataAnnotations;

namespace HelpCasa.Models
{
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
        public required string Password { get; set; }

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
        /// Área de especialização do empregado (opcional).
        /// </summary>
        public string? AreaOfExpertise { get; set; }

        /// <summary>
        /// Lista de experiências profissionais anteriores do empregado (opcional).
        /// </summary>
        public string? Experience { get; set; }

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
        public required string Password { get; set; }
    }

    /// <summary>
    /// DTO para atualizar a senha
    /// </summary>
    public class UpdatePasswordDto
    {
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
    }
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public required string NewPassword { get; set; }
    }

    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public string? AvailableTimeRange { get; set; }
        public string? AreaOfExpertise { get; set; }
        public string? Experience { get; set; }
        public List<string>? OfferedServices { get; set; }
        public required string UserType { get; set; }
    }

    public class EmployerResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public List<string>? ContractedServices { get; set; }
        public required string UserType { get; set; }
    }

    public class UpdateProfileRequestDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public string? AreaOfExpertise { get; set; } // Apenas para Empregados
        public string? Experience { get; set; } // Apenas para Empregados
        public string? AvailableTimeRange { get; set; } // Apenas para Empregados
    }


}