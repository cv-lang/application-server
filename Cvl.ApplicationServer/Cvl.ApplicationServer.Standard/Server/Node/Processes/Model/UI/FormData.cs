using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Dane opisujące pełną formatkę - widok i model
    /// służy do komunikacji procesu z frontem
    /// </summary>
    public class FormData
    {
        public FormData(){}
        public FormData(string formName, FormModel formModel)
        {
            FormName = formName;
            this.FormModel = formModel;
        }

        [Description("Nazwa widoku")]
        public string FormName { get; set; }

        //[Description("Model widoku")]
        //public BaseModel FormDataModel { get; set; }

        [Description("Model widoku")]
        public FormModel FormModel { get; set; }
        
    }
}
