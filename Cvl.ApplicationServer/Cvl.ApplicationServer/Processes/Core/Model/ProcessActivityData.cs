using System.ComponentModel.DataAnnotations.Schema;
using Cvl.ApplicationServer.Core.DataLayer.Model;

namespace Cvl.ApplicationServer.Processes.Core.Model
{
    /// <summary>
    /// Activity data - request and respons data
    /// all json's, xml's
    /// </summary>
    [Table("ProcessActivityData", Schema = "Processes")]
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
