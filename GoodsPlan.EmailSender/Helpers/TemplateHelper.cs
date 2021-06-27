using MimeKit;
using System.IO;

namespace GoodsPlan.EmailSender.Helpers
{
    public class TemplateHelper
    {
        public static string ReadTepmlateBody(string templateName)
        {
            var filePath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("\\"))
                            + Path.DirectorySeparatorChar.ToString()
                            + "GoodsPlan.EmailSender"
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + templateName + ".html";
            var bodyBuilder = new BodyBuilder();

            using (StreamReader streamReader = new StreamReader(filePath))
            {

                bodyBuilder.HtmlBody = streamReader.ReadToEnd();

            }

            return bodyBuilder.HtmlBody;
        }
    }
}
