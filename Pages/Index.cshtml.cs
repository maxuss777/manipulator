using System.Net;
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
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly AppSettings _appSettings;

        public IndexPageModel(
            ILogger<IndexPageModel> logger,
            IStringLocalizer<SharedResources> localizer,
            IOptions<AppSettings> appSettings)
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
                    mail.From = new MailAddress(Model.Email);
                    mail.To.Add(_appSettings.Email);
                    mail.Subject = Model.Subject;
                    mail.Body = GetBody();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(_appSettings.SenderEmail, _appSettings.SenderPass);
                        smtp.EnableSsl = true;

                        await smtp.SendMailAsync(mail);
                    }
                }

                return AjaxResponse.GetSuccessResponse(_localizer["EmailSendSuccess"].Value);
            }
            catch
            {
                return AjaxResponse.GetErrorResponse(_localizer["EmailSendError"].Value);
            }
        }

        private string GetBody()
        {
            return
            $"<b>{_localizer["Name"]}</b>: {Model.Name}<br> " +
            $"<b>{_localizer["Email"]}</b>: {Model.Email}<br> " +
            $"<b>{_localizer["Message"]}</b>: {Model.Message}";
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
