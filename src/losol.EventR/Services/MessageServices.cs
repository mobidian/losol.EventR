using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;

namespace losol.EventR.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<EmailSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public EmailSenderOptions Options { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Console.WriteLine("*** SEND EMAIL ASYNC ***");

            var _message = new MimeMessage();
            _message.From.Add(new MailboxAddress("losol.no", "ole@losol.no"));
            _message.To.Add(new MailboxAddress(email));
            _message.Subject = subject;

            _message.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.sendgrid.net", 587, false);
                Console.WriteLine("*** MAIL ***");
                Console.WriteLine("Tilkoblet? " + client.IsConnected.ToString() + ". Autentisert? " + client.IsAuthenticated.ToString());


                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(Options.SendGridUser, Options.SendGridPassword);
                Console.WriteLine("Tilkoblet? " + client.IsConnected.ToString() + ". Autentisert? " + client.IsAuthenticated.ToString());

                client.Send(_message);


                client.Disconnect(true);

                return Task.CompletedTask;

                /*
                 * The suggestion from microsoft: 
                var myMessage = new SendGrid.SendGridMessage();
                myMessage.AddTo(email);
                myMessage.From = new System.Net.Mail.MailAddress("no-reply@losol.no", "losol.no");
                myMessage.Subject = subject;
                myMessage.Text = message;
                myMessage.Html = message;
                var credentials = new System.Net.NetworkCredential(
                    Options.SendGridUser,
                    Options.SendGridKey);
                // Create a Web transport for sending email.
                var transportWeb = new SendGrid.Web(credentials);
                return transportWeb.DeliverAsync(myMessage);

                */
            }
        }



        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
