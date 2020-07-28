using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.AbstractionElements
{
    public interface IIOAbstraction
    {
        char DirectorySeparatorChar { get; }
        void CreateDirectory(string monthFolder);
        void AppendAllTextToFile(string fileName, string v);
        void CreateWindowsErrorLog(Exception ex, string applicationName, Guid? applicationNodeId);
        void ConsoleWriteLine(string text);
    }
}
