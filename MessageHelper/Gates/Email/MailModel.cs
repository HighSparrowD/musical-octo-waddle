#nullable disable
namespace MessageHelper.Gates.Email
{
    public class MailModel
    {
        public string Recipient { get; set; }
        public string RecipientName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentPath { get; set; }
    }
}
