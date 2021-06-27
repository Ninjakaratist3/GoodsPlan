using GoodsPlan.EmailSender.TemplateModels;
using System.Threading.Tasks;

namespace GoodsPlan.EmailSender
{
    public interface IEmailSender
    {
        Task SendAsync(TemplateModel message);
    }
}
