using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Manipulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Manipulator.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> _logger;
        private readonly IViewLocalizer _localizer;

        public IndexPageModel(ILogger<IndexPageModel> logger, IViewLocalizer localizer)
        {
            _logger = logger;
            _localizer = localizer;
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
                var fromAddress = new MailAddress("kulaistra.m@gmail.com", "Kulaistra");
                var toAddress = new MailAddress("kh.manipulator@gmail.com", "Kh.Manipulator");
                const string fromPassword = "kul!Q@W#Eaistra";
                const string subject = "test subject";
                const string body = "Hey now!!";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return AjaxResponse.GetSuccessResponse(_localizer["Your message has been sent. Thank you!"].Value);
            }
            catch
            {
                return AjaxResponse.GetSuccessResponse(_localizer["Sorry, something went wrong. Could you please try again later?"].Value);
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
