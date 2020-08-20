namespace Cvl.ApplicationServer.Contexts.Application.FrameworkAbstractions.AbstractionElements
{
    /// <summary>
    /// Warstwa abstrakcyjna dla .Net Framework
    /// aplikacja jest współdzielona między .Net Framework 4.5, .net framework 4.6.x,
    /// .net core 2.x, .net core 3
    /// Wspólnym mianownikiem jest .net Standard 1.2 (w którym jest napisana biblioteka),
    /// który nie zawiera wielu elementów API (configuracji, IO...)
    /// które są udostępnione przez tą warstwę abstrakcyjną .net frameworku
    /// </summary>
    public interface IConfigurationAbstraction
    {
        string GetAppSetting(string key);
        string GetConnectionString(string key);
    }
}
