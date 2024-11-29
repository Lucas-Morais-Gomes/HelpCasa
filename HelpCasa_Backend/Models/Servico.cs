namespace HelpCasa.Models
{
  /// <summary>
  /// Classe que representa um serviço oferecido por um empregado e contratado por um empregador.
  /// </summary>
  public class Service
  {
    /// <summary>
    /// Identificador único do serviço.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do serviço oferecido.
    /// </summary>
    public required string ServiceName { get; set; }

    /// <summary>
    /// Descrição detalhada do serviço.
    /// </summary>
    public string? ServiceDescription { get; set; }

    /// <summary>
    /// Preço do serviço oferecido.
    /// </summary>
    public decimal ServicePrice { get; set; }

    /// <summary>
    /// Localização onde o serviço será prestado.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Data e hora que o serviço será executado.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Referência ao empregado que oferece o serviço.
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Referência ao empregador que contrata o serviço.
    /// </summary>
    public Employer? Employer { get; set; }

    public required string Category { get; set; }

    public string? ImgUrl { get; set; }

    public required Boolean IsActive { get; set; }
  }
}
