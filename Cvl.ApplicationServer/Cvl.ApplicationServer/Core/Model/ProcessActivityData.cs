using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
{
    /// <summary>
    /// Activity data - request and respons data
    /// all json's, xml's
    /// </summary>
    public class ProcessActivityData : BaseEntity
    {
        public ProcessActivityData(string requestFullSerialization, string requestJson, string requestType)
        {
            RequestFullSerialization = requestFullSerialization;
            RequestJson = requestJson;
            RequestType = requestType;            
        }


        /// <summary>
        /// Full serializet of request
        /// to deserialize an object of type 'object'
        /// </summary>
        public string RequestFullSerialization { get; set; }

        /// <summary>
        /// Json serialize
        /// </summary>
        public string RequestJson { get; set; }

        /// <summary>
        /// Request full type name
        /// </summary>
        public string RequestType { get; set; }


        /// <summary>
        /// Full serializet of request
        /// to deserialize an object of type 'object'
        /// </summary>
        public string? ResponseFullSerialization { get; set; }

        /// <summary>
        /// Json serialize
        /// </summary>
        public string? ResponseJson { get; set; }

        /// <summary>
        /// Request full type name
        /// </summary>
        public string? ResponseType { get; set; }
    }
}
