using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWorkshop.Models;

namespace ProjectWorkshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BuildProject2024Context _context;

        public IndexModel(ILogger<IndexModel> logger, BuildProject2024Context context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            var patientName = _context.Patients.FirstOrDefault().FirstName;
        }
    }
}
