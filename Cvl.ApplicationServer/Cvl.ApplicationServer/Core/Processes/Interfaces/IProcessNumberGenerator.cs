namespace Cvl.ApplicationServer.Core.Processes.Interfaces
{
    public interface IProcessNumberGenerator
    {
        string GenerateProcessNumber(long processId);
    }
}
