using MimeKit;

namespace GoodsPlan.EmailSender.TemplateModels
{
    public abstract class TemplateModel
    {
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Subject { get; set; }

        public abstract TextPart GetMessageBody();
    }
}
