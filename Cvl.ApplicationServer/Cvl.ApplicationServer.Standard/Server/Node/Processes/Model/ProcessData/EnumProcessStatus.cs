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

        //Proes poprosił hosta/silnik procesów o jakieś dane - np. informację o statusie wykonania innego procesu
        WaitingForHost,
        //proces został wykonany
        Executed,
        //wystąpił błąd w trakcie wykonywania procesu
        Error
    }
}
