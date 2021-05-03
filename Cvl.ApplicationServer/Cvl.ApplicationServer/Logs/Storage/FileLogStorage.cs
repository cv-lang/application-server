using Cvl.ApplicationServer.Logs.Model;
using Cvl.ApplicationServer.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cvl.ApplicationServer.Logs.Storage
{
    public class LogContainer
    {
        public string FilePath { get; set; }
        public LogElement LogElement { get; set; }
    }

    public class FileLogStorage : LogStorageBase
    {
        public string StorageFolderPath { get; set; } = "c:\\logs\\";
        private string storageFile => StorageFolderPath + "logs.txt";
        public string logsDirectoryPath => StorageFolderPath + "byExternalId\\";

        private readonly string logsLineSeparator = $"___newLine_logs___{Environment.NewLine}";

        public FileLogStorage()
        {
            
        }

        public FileLogStorage(string logPath)
        {
            StorageFolderPath = logPath;
            Init();
        }

        public void Init()
        {
            Directory.CreateDirectory(StorageFolderPath);
            Directory.CreateDirectory(logsDirectoryPath);
        }


        public override void SaveLogs(LogElement logElement)
        {
            //zapisuje do nagłówka            
            var external1Dir = $"{logsDirectoryPath}{(logElement.Module != null ? logElement.Module + "_" : "" )}{logElement.ExternalId1}_{logElement.ExternalId2}_{logElement.ExternalId3}_log.txt";
            var logXml = Serializer.SerializeObject(logElement);

            //zapisuje ciało logu
            File.AppendAllText(external1Dir, logXml + logsLineSeparator);


            //File.AppendAllText
            logElement.Elements = null;
            var logContainer = new LogContainer();
            logContainer.LogElement = logElement;
            logContainer.FilePath = external1Dir;
            var logHeader = Serializer.SerializeObject(logContainer);
            File.AppendAllText(storageFile, logHeader + logsLineSeparator);
        }

        private List<LogContainer> getHeaders()
        {
            var list = new List<LogContainer>();
            var headerXml = File.ReadAllText(storageFile);
            var items = headerXml.Split(new string[] { logsLineSeparator }, StringSplitOptions.None);
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item) == false)
                {
                    var i = Serializer.DeserializeObject<LogContainer>(item);
                    list.Add(i);
                }
            }

            return list;
        }

        public override List<LogElement> GetHeaders()
        {
            return getHeaders().Select(x=> x.LogElement).ToList();
        }

        public override LogElement GetLogElement(string uniqueId)
        {
            var logs = getHeaders();
            
            return getLog(uniqueId, logs);
        }

        private LogElement getLog(string uniqueId, List<LogContainer> logs)
        {
            var ids = uniqueId.Split('-');

            if(ids.Length == 1)
            {
                return getFirstLevelLogs(uniqueId, logs);
            } else
            {
                //mamy złożony id, gdzie kolejne zagniezdrzenia sa oddzielone -
                var p = ids.ToList();
                p.RemoveAt(p.Count-1);
                var preId = string.Join("-", p);
                var preLog = getLog(preId, logs);
                return preLog.Elements.FirstOrDefault(x => x.UniqueId == uniqueId);
            }
        }


        private LogElement getFirstLevelLogs(string uniqueId, List<LogContainer> logs)
        {
            var logElement = logs.FirstOrDefault(x => x.LogElement.UniqueId == uniqueId);
            if (logElement == null)
            {
                return null;
            }

            var logsXml = File.ReadAllText(logElement.FilePath);
            var items = logsXml.Split(new string[] { logsLineSeparator }, StringSplitOptions.None);
            foreach (var item in items)
            {
                var i = Serializer.DeserializeObject<LogElement>(item);
                if (i.UniqueId == uniqueId)
                {
                    return i;
                }
            }

            return null;
        }
    }
}
