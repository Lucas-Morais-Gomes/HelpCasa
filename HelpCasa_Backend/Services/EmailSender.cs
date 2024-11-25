using RestSharp;

namespace HelpCasa.Services
{
    public class MailgunEmailSender
    {
        public static RestResponse SendEmail(string toEmail, string subject, string body)
        {
            // Instancia o cliente RestSharp com a URL base
            RestClient client = new RestClient("https://api.mailgun.net/v3");

            // Cria a requisição
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox563c835b37634b9e9cc2fe91d5d4469e.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            // Adiciona cabeçalhos manualmente para a autenticação
            string authHeader = "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("api:5c23b41b306777f55e9a2c9e8ea20007-6df690bb-5e53708d"));
            request.AddHeader("Authorization", authHeader);  // Adiciona o cabeçalho de autenticação

            // Parâmetros do e-mail
            request.AddParameter("from", $"HelpCasa Suporte <helpcasa.support@gmail.com>");
            request.AddParameter("to", toEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("text", body);

            // Define o método como POST
            request.Method = Method.Post;

            // Executa a requisição e retorna a resposta
            return client.Execute(request);
        }
    }
}
