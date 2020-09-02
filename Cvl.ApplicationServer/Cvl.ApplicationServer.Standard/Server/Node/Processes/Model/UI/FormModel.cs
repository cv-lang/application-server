using Cvl.ApplicationServer.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    /// <summary>
    /// Model widoku - taki kontener modelu z dodatkowymi potrzebnymi danymi.
    /// Zawiera propercje Model w której jest fizyczny obiekt z danymi.
    /// Obiekt przekazywany do formatki.
    /// </summary>
    public class FormModel
    {
        private const string gm = "Dane bazowe";

        [DataForm(GroupName = gm, Description = "Model widoku", Order = 10001)]
        //public virtual object BaseModel { get; internal set; }
        public virtual void SetModel(object model)
        { }
        public virtual object GetModel()
        {
            return null;
        }


        [DataForm(GroupName = gm, Description = "Id procesu", Order = 10001)]
        public long ProcessId { get; set; }

        [DataForm(GroupName = gm, Description = "Nazwa layoutu widoku", Order = 10001)]
        public string Layout { get; set; } = "_Layout";

        [DataForm(GroupName = gm, Description = "Adres IP Klienta", Order = 10001)]
        public string ClientIpAddress { get; set; }
    }

    public class FormModel<T> : FormModel
        where T: new()        
    {
        private const string gm = "Dane bazowe";

        [DataForm(GroupName = gm, Description = "Model widoku", Order = 10001)]
        public T Model { get; set; } = new T();

        public override object GetModel()
        {
            return Model;
        }

        public override void SetModel(object model)
        {
            Model = (T)model;
        }

        //public override object BaseModel { get => Model; internal set => Model = (T)value; }
    }
}
