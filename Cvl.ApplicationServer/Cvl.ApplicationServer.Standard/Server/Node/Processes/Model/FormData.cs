using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public class FormData
    {
        public FormData(){}
        public FormData(string formName, BaseModel formModel)
        {
            FormName = formName;
            this.FormDataModel = formModel;
        }

        [Description("Nazwa widoku")]
        public string FormName { get; set; }

        [Description("Model widoku")]
        public BaseModel FormDataModel { get; set; }
        
    }
}
