using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model.Processes
{
    public enum ProcessActivityState
    {
        Executing,
        Executed,
        Exception
    }

    /// <summary>
    /// History of interaction with process
    /// Contains request and respons of interaction
    /// in json and xml
    /// </summary>
    /// 
    [Table("ProcessActivity", Schema = "Processes")]
    public class ProcessActivity : BaseEntity
    {
        public ProcessActivity(long? processInstanceId,
            string clientIpAddress, string clientIpPort, string clientConnectionData,
            ProcessActivityState activityState, string memberName,
            DateTime requestDate, string previewRequestJson,
            DateTime? responseDate, string? previewResponseJson)
        {
            ProcessInstanceId = processInstanceId;
            ClientIpAddress = clientIpAddress;
            ClientIpPort = clientIpPort;
            ClientConnectionData = clientConnectionData;
            RequestDate = requestDate;
            PreviewRequestJson = previewRequestJson;
            ResponseDate = responseDate;
            PreviewResponseJson = previewResponseJson;
            MemberName = memberName;
            ActivityState = activityState;
        }

        public const int JsonPreviewSize = 150;
        public long? ProcessInstanceId { get; set; }
        public virtual ProcessInstance? ProcessInstance { get; set; }

        /// <summary>
        /// Dane połączenia klienta
        /// </summary>
        public string ClientIpAddress { get; set; }
        public string ClientIpPort { get; set; }
        public string ClientConnectionData { get; set; }

        public ProcessActivityState ActivityState { get; set; }
        public string MemberName { get; set; }

        /// <summary>
        /// Data requestu
        /// </summary>
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [StringLength(JsonPreviewSize)]
        public string PreviewRequestJson { get; set; }


        public DateTime? ResponseDate { get; set; }

        [StringLength(JsonPreviewSize)]
        public string? PreviewResponseJson { get; set; }


        public long ProcessActivityDataId { get; set; }
        public ProcessActivityData ProcessActivityData { get; set; } = null!;
    }
}
