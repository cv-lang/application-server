using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Dane związane z wyświetlaniem 
    /// </summary>
    public class ProcessUI
    {
        public ProcessUI()
        {
            WaitingForm = new FormData();
            WaitingForm.FormName = "WaitingView";
            WaitingForm.FormDataModel = new GeneralViewModel();
            WaitingForm.FormDataModel.Layout = ViewLayout;
        }

        public void SetBaseViewPath(string path)
        {
            BaseViewPath = path;
            WaitingForm.FormName = BaseViewPath + "WaitingView";
            WaitingForm.FormDataModel.Layout = ViewLayout;
        }

        private const string gm = "Proces UI";

        [DataForm(GroupName = gm, Description = "Ścieżka do folderu widoków - dodawana jest do nazwy widoku")]
        public string BaseViewPath { get; set; }

        [DataForm(GroupName = gm, Description = "Nazwa layoutu widoku")]
        public string ViewLayout { get; set; } = "_Layout";


        [DataForm(GroupName = gm, Description = "Dane które są wyświetlane użytkownikowi")]
        public FormData FormDataToShow { get; set; }

        [DataForm(GroupName = gm, Description = "Pobrane dane od użytkownika")]
        public FormData FormDataFromUser { get; set; }

        public FormData WaitingForm { get; set; }
    }
}
