using System.ComponentModel.DataAnnotations;

namespace HelpCasa.Models
{
  public abstract class Usuario
  {
    public int Id { get; set; }
    public string Nome { get; set; } // Obrigatório
    public string Email { get; set; } // Obrigatório
    public string SenhaHash { get; set; } // Obrigatório
    public string Endereco { get; set; } // Obrigatório
    public string? FotoPerfil { get; set; }
    public string? Descricao { get; set; } // 
    public string Telefone { get; set; } // Obrigatório
    public int Avaliacao { get; set; } // Adicional
  }

  public class Empregado : Usuario
  {
    public string FaixaHorarioDisponivel { get; set; }
    public string AreaLimiteAtuacao { get; set; }
    public List<Servico> ServicosOferecidos { get; set; } 
    public int Experiencia { get; set; } // Adicional
  }

  public class Empregador : Usuario
  {
    public List<Servico> ServicosContratados { get; set; } 
  }

  public class UsuarioDto
  {
    [Required(ErrorMessage = "Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Endereço é obrigatório.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "Telefone é obrigatório.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Tipo de usuário é obrigatório.")]
    public string TipoUsuario { get; set; } // "Empregado" ou "Empregador"
  }


  public class UsuarioLoginDto
  {
    public string Email { get; set; } // Apenas Email
    public string Senha { get; set; } // Apenas Senha
  }


}
