using GoodsPlan.EmailSender.Helpers;
using MimeKit;
using System;

namespace GoodsPlan.EmailSender.TemplateModels
{
    public class RegistrationTemplateModel : TemplateModel
    {
        public override TextPart GetMessageBody()
        {
            var messageBody = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = String.Format(TemplateHelper.ReadTepmlateBody("Registration"), this.UserName)
            };
            return messageBody;
        }
    }
}
