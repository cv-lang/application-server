namespace Cvl.ApplicationServer.Server.Node.Processes.Model
{
    public enum EnumProcessStatus
    {
        //Proces oczekuje na wykonanie - jest skolejkowany
        WaitingForExecution,
        //proces jest wykonywany
        Running,
        //proces oczekuje na dane od użytkownika
        WaitingForUserData,
        //proces został wykonany
        Executed,
        //wystąpił błąd w trakcie wykonywania procesu
        Error
    }
}
