using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace MessageHelper.Gates.SMS
{
    public class TwilioSender
    {
        public string Sender { get; set; }
        private string TwillioUsername { get; }
        private string TwillioPassword { get; }

        public TwilioSender()
        {
            var credentials = new Credentials();

            Sender = credentials.DefaultSender;

            TwillioUsername = credentials.TwilioUsername;
            TwillioPassword = credentials.TwilioPassword;
        }

        public TwilioSender(string sender)
        {
            var credentials = new Credentials();

            Sender = sender;

            TwillioUsername = credentials.TwilioUsername;
            TwillioPassword = credentials.TwilioPassword;
        }

        public TwilioSender(string sender, string twillioUsername, string twillioPassword)
        {
            Sender = sender;
            TwillioUsername = twillioUsername;
            TwillioPassword = twillioPassword;
        }

        public Status SendMessage(string receiver, string body)
        {
            TwilioClient.Init(TwillioUsername, TwillioPassword);

            var message = MessageResource.Create(new CreateMessageOptions(new PhoneNumber(receiver))
            {
                From = Sender,
                Body = body,
            });

            return Status.Sent;
        }

        public async Task<Status> SendMessageAsync(string receiver, string body)
        {
            TwilioClient.Init(TwillioUsername, TwillioPassword);

            var message = await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(receiver))
            {
                From = Sender,
                Body = body,
            });

            return Status.Sent;
        }
    }
}
