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
        private IInstanceCreator _defaultInstanceCreator;

        public ServiceProviderInstanceCreator(IServiceProvider serviceProvider, IInstanceCreator instanceCreator)
        {
            this._serviceProvider = serviceProvider;
            _defaultInstanceCreator = instanceCreator;
        }

        public object CreateInstance(Type type)
        {
            var instance =  _serviceProvider.GetService(type);
            if (instance == null)
            {
                instance = _defaultInstanceCreator.CreateInstance(type);
            }

            return instance;
        }
    }
}
