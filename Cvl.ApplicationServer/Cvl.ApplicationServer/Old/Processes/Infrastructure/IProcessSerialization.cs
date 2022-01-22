using Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Interfaces
{
    public interface IProcessSerialization
    {
        string ProcessSerizalization(IFullSerializer serializer);
        void ProcessDeserialization(IFullSerializer serializer, string serializedProcess);
    }
}
