using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polenter.Serialization.Core;

namespace ConsoleApp1.TTeeesd
{
    public class ServiceProviderInstanceCreator : IInstanceCreator
    {
        private IServiceProvider _serviceProvider;

        public ServiceProviderInstanceCreator(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public object CreateInstance(Type type)
        {
            var instance =  _serviceProvider.GetService(type);
            if (instance == null)
            {
                instance = Activator.CreateInstance(type);
                //instance = _defaultInstanceCreator.CreateInstance(type);
            }

            return instance;
        }
    }
}
