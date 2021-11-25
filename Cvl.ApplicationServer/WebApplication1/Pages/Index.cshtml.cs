using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private int id;
        public IndexModel(ILogger<IndexModel> logger, int id = 2)
        {
            _logger = logger;
            this.id = id;
        }

        public void OnGet()
        {

        }
    }   
}