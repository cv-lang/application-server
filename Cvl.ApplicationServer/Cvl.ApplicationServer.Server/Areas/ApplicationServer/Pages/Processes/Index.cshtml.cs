using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class IndexModel : PageModel
    {        
        private readonly Core.ApplicationServer _applicationServer;

        public IndexModel(Core.ApplicationServer applicationServer)
        {
            this._applicationServer = applicationServer;
        }

        public void OnGet()
        {
        }       
    }
}
