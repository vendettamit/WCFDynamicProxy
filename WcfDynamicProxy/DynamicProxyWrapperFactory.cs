using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfDynamicProxy
{
    public class DynamicProxyWrapperFactory
    {
        private static ProxyGenerator proxyGenerator = new ProxyGenerator();

        public static TIServiceContractInterface GetProxyInstanceByInterface<TIServiceContractInterface>(string serviceName)
        {
            return GetProxyInstanceByInterface<TIServiceContractInterface>(new WcfClientInvocationInterceptor<TIServiceContractInterface>(serviceName));
        }


        private static TIServiceContractInterface GetProxyInstanceByInterface<TIServiceContractInterface>(IInterceptor clientProxyInvoker)
        {
           return (TIServiceContractInterface)proxyGenerator
                    .CreateInterfaceProxyWithoutTarget(typeof(TIServiceContractInterface), new ProxyGenerationOptions(new DefaultProxyGenHook()), clientProxyInvoker);
        }
    }
}
