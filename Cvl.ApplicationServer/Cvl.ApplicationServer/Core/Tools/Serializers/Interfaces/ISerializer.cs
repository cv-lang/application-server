using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces
{
    /// <summary>
    /// Object serializer
    /// </summary>
    public interface ISerializer
    {
        string Serialize(object obj);

        T? Deserialize<T>(string json);
    }
}
