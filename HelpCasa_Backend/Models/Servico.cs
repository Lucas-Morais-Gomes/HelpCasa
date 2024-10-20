namespace HelpCasa.Models
{
  public class Service
  {
    public int Id { get; set; } // Adicione um Id para identificar o serviço
    public string ServiceName { get; set; } // name do serviço
    public string ServiceDescription { get; set; } // Descrição do serviço
    public decimal ServicePrice { get; set; } // Preço do serviço
    public string Location { get; set; } // Localização do serviço
    public DateTime DateTime { get; set; } // Data e hora do serviço
    public Employee Employee { get; set; } // Referência ao Employee que oferece o serviço
    public Employer Employer { get; set; } // Referência ao empregador que contrata o serviço
  }
}
