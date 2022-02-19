using Cvl.ApplicationServer.Core.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cvl.ApplicationServer.Server.Areas.ApplicationServer.Pages.Processes
{
    public class IndexModel : PageModel
    {        
        private readonly IApplicationServer _applicationServer;

        public IndexModel(IApplicationServer applicationServer)
        {
            this._applicationServer = applicationServer;
        }

        public void OnGet()
        {
        }       
    }
}
