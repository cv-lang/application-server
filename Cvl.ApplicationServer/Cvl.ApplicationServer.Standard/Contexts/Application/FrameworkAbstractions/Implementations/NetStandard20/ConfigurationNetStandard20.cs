using Cvl.ApplicationServer.Contexts.FrameworkAbstractions.AbstractionElements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Cvl.ApplicationServer.Contexts.FrameworkAbstractions.Implementations.NetStandard20
{
    public class ConfigurationNetStandard20 : IConfigurationAbstraction
    {
        private FrameworkNetStandard20 frameworkNetCore22;

        public ConfigurationNetStandard20(FrameworkNetStandard20 frameworkNetCore22)
        {
            this.frameworkNetCore22 = frameworkNetCore22;
        }

        public string GetAppSetting(string key)
        {
            var conf = ConfigurationManager.AppSettings[key];
            return conf;
        }        

        public string GetConnectionString(string key)
        {
            var con = ConfigurationManager.ConnectionStrings[key];
            return con.ConnectionString;
            //return System.Configuration.ConfigurationManager
            //    .ConnectionStrings[key]?.ConnectionString;
        }        
    }
}
