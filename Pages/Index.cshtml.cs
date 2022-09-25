using System.Net.Mail;
using System.Threading.Tasks;
using Manipulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Manipulator.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> _logger;
        private readonly IStringLocalizer<IndexPageModel> _localizer;
        private readonly AppSettings _appSettings;

        public IndexPageModel(ILogger<IndexPageModel> logger, IStringLocalizer<IndexPageModel> localizer, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _localizer = localizer;
            _appSettings = appSettings.Value;
        }

        [BindProperty]
        public ContactUsViewModel Model { get; set; }

        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostContactUsAsync()
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(_appSettings.Email);
                    mail.From = new MailAddress(Model.Email);
                    mail.Subject = Model.Subject;
                    mail.Body = Model.Message;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("kulaistra.m@outlook.com", "kul!Q@W#Eaistra");
                    smtp.Send(mail);
                }

                return AjaxResponse.GetSuccessResponse(_localizer["EmailSendSuccess"].Value);
            }
            catch
            {
                return AjaxResponse.GetSuccessResponse(_localizer["EmailSendError"].Value);
            }
        }
    }

    public class ContactUsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
