namespace Cvl.ApplicationServer.Contexts.Application.Elements
{
    /// <summary>
    /// Konfiguracja aplikacji
    /// </summary>
    public class ApplicationConfiguration
    {
        private ApplicationContext applicationContext;
        public ApplicationConfiguration(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public string GetAppSetting(string key, string defaultValue = null)
        {
            var val = applicationContext.Framework.Configuration.GetAppSetting(key);
            if (string.IsNullOrEmpty(val))
            {
                return defaultValue;
            }
            else
            {
                return val;
            }
        }

        internal void Initialize()
        {
            //throw new NotImplementedException();
        }
    }
}
