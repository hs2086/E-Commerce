using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receptor, string Sub, string body);
    }

    public class EmailService : IEmailService
    {

        public async Task SendEmailAsync(string receptor, string Sub, string body)
        {
            var host = Environment.GetEnvironmentVariable("HOST");
            var port = Environment.GetEnvironmentVariable("PORT");
            var emailUser = Environment.GetEnvironmentVariable("EMAIL");
            var password = Environment.GetEnvironmentVariable("PASSWORD");

            var smtpClient = new SmtpClient(host, int.Parse(port));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(emailUser, password);

            string Subject = Sub;
            string Body = body;
            var message = new MailMessage(emailUser, receptor, Subject, Body)
            {
                IsBodyHtml = true
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}
