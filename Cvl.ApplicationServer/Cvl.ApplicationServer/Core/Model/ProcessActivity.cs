using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Model
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
    public class ProcessActivity : BaseEntity
    {
        public ProcessActivity(long? processInstanceId,
            string clientIpAddress, string clientIpPort, string clientConnectionData,
            ProcessActivityState activityState, string memberName,
            DateTime requestDate, string previewRequestJson, 
            DateTime? responseDate, string? previewResponseJson, 
            long processActivityDataId)
        {
            ProcessInstanceId = processInstanceId;            
            ClientIpAddress = clientIpAddress;
            ClientIpPort = clientIpPort;
            ClientConnectionData = clientConnectionData;
            RequestDate = requestDate;
            PreviewRequestJson = previewRequestJson;
            ResponseDate = responseDate;
            PreviewResponseJson = previewResponseJson;
            ProcessActivityDataId = processActivityDataId;
            MemberName = memberName;
            ActivityState = activityState;
        }

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

        [StringLength(150)]
        public string PreviewRequestJson { get; set; }


        public DateTime? ResponseDate { get; set; }

        [StringLength(150)]
        public string? PreviewResponseJson { get; set; }


        public long ProcessActivityDataId { get; set; }
        public ProcessActivityData ProcessActivityData { get; set; }
    }
}
