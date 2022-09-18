using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Manipulator.Pages
{
    public class IndexPageModel : PageModel
    {
        private readonly ILogger<IndexPageModel> _logger;

        public IndexPageModel(ILogger<IndexPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
