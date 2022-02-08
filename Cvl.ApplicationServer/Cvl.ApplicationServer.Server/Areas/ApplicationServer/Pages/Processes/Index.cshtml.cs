using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class IndexModel : PageModel
    {        
        private readonly Core.ApplicationServerOld _applicationServer;

        public IndexModel(Core.ApplicationServerOld applicationServer)
        {
            this._applicationServer = applicationServer;
        }

        public void OnGet()
        {
        }       
    }
}
