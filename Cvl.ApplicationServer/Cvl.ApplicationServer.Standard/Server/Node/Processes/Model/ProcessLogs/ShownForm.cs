using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    public enum ShownFormType
    {
        ShownToUser,
        FromUser
    }

    public class ShownForm
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ShownFormType ShownFormType { get; set; }
        public FormData FormModel { get; internal set; }
    }
}
