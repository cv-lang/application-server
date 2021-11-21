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
            id = id;
        }

        public void OnGet()
        {

        }
    }

    public class DetailProductViewModel
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
    }
}