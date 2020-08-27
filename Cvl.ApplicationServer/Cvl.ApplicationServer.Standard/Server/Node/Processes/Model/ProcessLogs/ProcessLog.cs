using Cvl.ApplicationServer.Monitoring.Base;
using Cvl.ApplicationServer.Tools.Extension;
using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model.ProcessLogs
{
    [Description("Magazyn danych historycznych procesu, zmiany stanu, logi, zewnętrzne wywołania")]
    public class ProcessLog
    {
        [Description("Logi wykonania procesu")]
        public Logger Logger { get; set; } = new Logger();

        [Description("Historyczny zapis zmiany stanów procesu")]
        public List<StepData> StepsData { get; set; } = new List<StepData>();

        [Description("Historyczna komunikacja z zewnętrznymi systemami")]
        public List<ExternalCommunication> ExternalCommunications { get; set; } = new List<ExternalCommunication>();

        [Description("Historyczne dane wyświetlane Klientowi oraz dane wpisane przez Klienta")]
        public List<ShownForm> ShownForms { get; set; } = new List<ShownForm>();

        [Description("Historyczne dane uruchomionych procesów")]
        public List<ChildProcess> ChildProcesses { get; set; } = new List<ChildProcess>();



        internal void AddShowForm(FormData formModel, ShownFormType shownFormType)
        {
            var sf = new ShownForm();
            sf.ShownFormType = shownFormType;
            sf.FormModel = formModel;
            ShownForms.Add(sf);
        }

        internal void AddStep(string step, string stepDescription)
        {
            var s = new StepData();
            s.StepName = step;
            s.StepDescription = stepDescription;
            StepsData.Add(s);
        }

        internal void AddChildProcess(BaseProcess childProcess)
        {
            var cp = new ChildProcess();
            var type = childProcess.GetType();
            cp.Name = type.Name;
            cp.FullName = type.FullName;
            cp.Description = type.GetClassDescription();

            ChildProcesses.Add(cp);
        }

        internal void AddExternalCommunication(string type, string header, string content, object[] parameters)
        {
            var e = new ExternalCommunication();
            e.Type = type;
            e.Header = header;
            e.Content = content;
            e.Parameters = string.Join("; ", parameters);

            ExternalCommunications.Add(e);
        }
    }
}
