namespace HelpCasa.Models
{
  public class Servico
  {
    public int Id { get; set; } // Adicione um Id para identificar o serviço
    public string NomeServico { get; set; } // Nome do serviço
    public string DescricaoServico { get; set; } // Descrição do serviço
    public decimal PrecoServico { get; set; } // Preço do serviço
    public string Localizacao { get; set; } // Localização do serviço
    public DateTime DataHora { get; set; } // Data e hora do serviço
    public Empregado Empregado { get; set; } // Referência ao empregado que oferece o serviço
    public Empregador Empregador { get; set; } // Referência ao empregador que contrata o serviço
  }
}
