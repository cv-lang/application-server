using Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.AbstractionElements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.Implementations.NetStandard20
{
    public class IONetStandard20 : IIOAbstraction
    {
        public char DirectorySeparatorChar => Path.DirectorySeparatorChar;

        public void AppendAllTextToFile(string fileName, string text)
        {
            File.AppendAllText(fileName, text);
        }

        public void ConsoleWriteLine(string text)
        {
            System.Console.WriteLine(text);
        }

        public void CreateDirectory(string name)
        {
            Directory.CreateDirectory(name);
        }


        /// <summary>
        /// Obsługa krytycznych błędów aplikacji - takich które uniemożliwiają start aplikacji
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="applicationName"></param>
        /// <param name="applicationNodeId"></param>
        public void CreateWindowsErrorLog(Exception ex, string applicationName, Guid? applicationNodeId)
        {
            //try
            //{
            //    using (EventLog eventLog = new EventLog("Application"))
            //    {                    
            //        eventLog.Source = "Application";
            //        eventLog.WriteEntry($"Cvl.Exception - {applicationName}({applicationNodeId}) - {ex.Message}", EventLogEntryType.Error, 0,1);
            //    }
            //} catch(Exception exc)
            //{
            //    //jeśli tu się wywali, to mamy ogólnie problem z zalogowaniem błędu, pozostaje rzucenie wyjątku
            //    throw new Exception("Nie można zapisac błędu w dzienniku windows - błąd może został zapisany do pliku z logami", ex);
            //}            
        }
    }
}
