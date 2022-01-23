using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Tools.Serializers.Interfaces
{
    /// <summary>
    /// Serialize full object
    /// can deserialize type 'object' by save object full type name
    /// </summary>
    public interface IFullSerializer : ISerializer
    {
    }
}
