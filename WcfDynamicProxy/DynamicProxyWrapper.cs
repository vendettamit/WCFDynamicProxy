using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WcfDynamicProxy
{
    internal sealed class DefaultProxyGenHook : IProxyGenerationHook
    {
        /// <summary>
        /// Default proxy generator allows all the methods to be intercepted.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>
        /// True if the method should should be
        /// intercepted, otherwise false.
        /// </returns>
        public bool InterceptMethod(MethodInfo methodInfo)
        {
            return true;
        }

        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, System.Reflection.MemberInfo memberInfo)
        {
        }

        public bool ShouldInterceptMethod(Type type, System.Reflection.MethodInfo methodInfo)
        {
            return true;
        }
    }
}
