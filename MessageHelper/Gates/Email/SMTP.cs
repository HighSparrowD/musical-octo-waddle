using MailKit.Net.Smtp;
using MimeKit;

#nullable disable
namespace MessageHelper.Gates.Email
{
    public class SMTP
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
        private string[] _awailableHosts => new string[] {"seznam.cz"};

        public SMTP(string sender, string password)
        {
            SmtpHost = "smtp.seznam.cz";
            SmtpPort = 465;
            Sender = sender;
            Password = password;
        }

        public SMTP(string sender, string senderName, string password)
        {
            SmtpHost = "smtp.seznam.cz";
            SmtpPort = 465;
            Sender = sender;
            SenderName = senderName;
            Password = password;
        }

        public SMTP(string smtpHostAddress, int smtpPort, string sender, string password)
        {
            SmtpHost = smtpHostAddress;
            SmtpPort = smtpPort;
            Sender = sender;
            Password = password;
        }

        public SMTP(string smtpHostAddress, int smtpPort, string sender, string senderName, string password)
        {
            SmtpHost = smtpHostAddress;
            SmtpPort = smtpPort;
            Sender = sender;
            SenderName = senderName;
            Password = password;
        }

        public Status SendEmail(string recipient, string recipientName, string subject, string body, string attachmentPath)
        {
            if (!_awailableHosts.Any(h => Sender.Contains(h)))
                throw new ArgumentException($"Email {Sender} cannot be used to send emails. Providers available now are: {string.Join(", ",_awailableHosts)}");

            var email = new MimeMessage();
            var builder = new BodyBuilder();

            builder.Attachments.Add(attachmentPath);
            builder.TextBody = body;

            email.From.Add(new MailboxAddress(string.IsNullOrEmpty(SenderName)? "Sender" : SenderName, Sender));
            email.To.Add(new MailboxAddress(string.IsNullOrEmpty(recipientName)? "Recipient" : recipientName, recipient));

            email.Subject = subject;

            email.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Timeout = 10000;

                client.Connect(SmtpHost, SmtpPort, true);
                client.Authenticate(Sender, Password);

                client.Send(email);
                client.Disconnect(true);

                return Status.Sent;
            }

        }

        public Status SendEmail(MailModel model)
        {
            if (!_awailableHosts.Any(h => Sender.Contains(h)))
                throw new ArgumentException($"Email {Sender} cannot be used to send emails. Providers available now are: {string.Join(", ", _awailableHosts)}");

            var email = new MimeMessage();
            var builder = new BodyBuilder();

            if (!string.IsNullOrEmpty(model.AttachmentPath))
                builder.Attachments.Add(model.AttachmentPath);

            builder.TextBody = model.Body;

            email.From.Add(new MailboxAddress(string.IsNullOrEmpty(SenderName) ? "Sender" : SenderName, Sender));
            email.To.Add(new MailboxAddress(string.IsNullOrEmpty(model.RecipientName) ? "Recipient" : model.RecipientName, model.Recipient));

            email.Subject = model.Subject;

            email.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Timeout = 10000;

                client.Connect(SmtpHost, SmtpPort, true);
                client.Authenticate(Sender, Password);

                client.Send(email);
                client.Disconnect(true);

                return Status.Sent;
            }

        }

        public async Task<Status> SendEmailAsync(string recipient, string recipientName, string subject, string body, string attachmentPath)
        {
            if (!_awailableHosts.Any(h => Sender.Contains(h)))
                throw new ArgumentException($"Email {Sender} cannot be used to send emails. Providers available now are: {string.Join(", ", _awailableHosts)}");

            var email = new MimeMessage();
            var builder = new BodyBuilder();

            await builder.Attachments.AddAsync(attachmentPath);
            builder.TextBody = body;

            email.From.Add(new MailboxAddress(string.IsNullOrEmpty(SenderName) ? "Sender" : SenderName, Sender));
            email.To.Add(new MailboxAddress(string.IsNullOrEmpty(recipientName) ? "Recipient" : recipientName, recipient));

            email.Subject = subject;

            email.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Timeout = 10000;

                await client.ConnectAsync(SmtpHost, SmtpPort, true);
                await client.AuthenticateAsync(Sender, Password);

                await client.SendAsync(email);
                await client.DisconnectAsync(true);

                return Status.Sent;
            }

        }

        public async Task<Status> SendEmailAsync(MailModel model)
        {
            if (!_awailableHosts.Any(h => Sender.Contains(h)))
                throw new ArgumentException($"Email {Sender} cannot be used to send emails. Providers available now are: {string.Join(", ", _awailableHosts)}");

            var email = new MimeMessage();
            var builder = new BodyBuilder();

            if (!string.IsNullOrEmpty(model.AttachmentPath))
                await builder.Attachments.AddAsync(model.AttachmentPath);

            builder.TextBody = model.Body;

            email.From.Add(new MailboxAddress(string.IsNullOrEmpty(SenderName) ? "Sender" : SenderName, Sender));
            email.To.Add(new MailboxAddress(string.IsNullOrEmpty(model.RecipientName) ? "Recipient" : model.RecipientName, model.Recipient));

            email.Subject = model.Subject;

            email.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                client.Timeout = 10000;

                await client.ConnectAsync(SmtpHost, SmtpPort, true);
                await client.AuthenticateAsync(Sender, Password);

                await client.SendAsync(email);
                await client.DisconnectAsync(true);

                return Status.Sent;
            }

        }
    }
}
