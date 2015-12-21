using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfDynamicProxy
{
    internal class WcfClientInvocationInterceptor<TIServiceContract> : IInterceptor
    {
        public string hostServiceName { get; set; }

        /// <summary>
        /// Constructor of <see cref="WcfClientInvocationInterceptor<TIServiceContract>"/>
        /// </summary>
        /// <param name="hostServiceName">Name of host for information purpose</param>
        /// <param name="endpointName">Name of defined endpoint in configuration file.</param>
        public WcfClientInvocationInterceptor(string hostServiceName)
        {
            // set the name of the corresponding hostService
            this.hostServiceName = hostServiceName;
        }

        /// <summary>
        /// Implementation of Castle dynamic proxy interceptor.
        /// </summary>
        /// <param name="target"></param>
        public void Intercept(IInvocation target)
        {
            try
            {
                this.Invocation(target);
            }
            catch (Exception e)
            {
                // Rest the stack to this point only, any error occurs.
                throw e;
            }

        }

        protected void Invocation(IInvocation target)
        {
            // Create an instance
            ChannelFactory<TIServiceContract> channelFactory = null;

            try
            {

                // create client with channelfactory
                channelFactory = new ChannelFactory<TIServiceContract>(this.hostServiceName);


                TIServiceContract channel = channelFactory.CreateChannel();

                object retVal = this.ServiceCall(target, channel);

                target.ReturnValue = retVal;

                channelFactory.Close();
            }
            finally
            {
                if (channelFactory != null)
                {
                    ((IDisposable)channelFactory).Dispose();
                }
            }
        }

        private object ServiceCall(IInvocation target, object channel)
        {
            Type type = channel.GetType();

            MethodInfo methodInfo = type.GetMethod(target.Method.Name);

            if (methodInfo == null)
            {
                foreach (var typeInterface in type.GetInterfaces())
                {
                    methodInfo = typeInterface.GetMethod(target.Method.Name);
                    break;
                }
            }

            if (methodInfo == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "The method {0} could not be found on given operation contract.",
                        target.Method.Name));
            }

            if (target.Method.IsGenericMethod)
            {
                methodInfo = methodInfo.MakeGenericMethod(target.Method.GetGenericArguments());
            }

            return methodInfo.Invoke(
                channel,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance,
                null,
                target.Arguments,
                CultureInfo.InvariantCulture);
        }
    }
}
