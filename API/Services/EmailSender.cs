using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Services
{
  public class EmailSender
  {
    private readonly IConfiguration _config;
    public EmailSender(IConfiguration config)
    {
      _config = config;
    }

    public virtual async Task SendEmail(string destEmail, string subject, string messageBody)
    {
      var key = _config["SenderApiKey"];
      var client = new SendGridClient(key);
      var from = new EmailAddress("leandro.righeti@gmail.com", "Financial Chat");
      var to = new EmailAddress(destEmail);
      var plainText = messageBody;
      var html = messageBody;
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, html);
      var response = await client.SendEmailAsync(msg);
    }
  }
}