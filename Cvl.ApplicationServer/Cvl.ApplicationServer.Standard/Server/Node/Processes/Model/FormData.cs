using System;
using System.Collections.Generic;
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

        public string FormName { get; set; }
        public BaseModel FormDataModel { get; set; }
    }
}
