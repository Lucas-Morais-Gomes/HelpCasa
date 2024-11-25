namespace HelpCasa.Services
{
    public class MailgunSettings
    {
        public required string ApiKey { get; set; }
        public required string Domain { get; set; }
        public required string SenderEmail { get; set; }
    }

}
