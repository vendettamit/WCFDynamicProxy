using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WcfDynamicProxy.Tests.Helper
{
    /// <summary>
    /// Invokes the process that host the test service required for testing wcf client.
    /// </summary>
    internal class ServiceHostProcessInvoker
    {
        /// <summary>
        /// Invokes the host process for test service
        /// </summary>
        public static void InvokeDummyService()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(path, "DummyService.exe"));

            info.UseShellExecute = true;
            info.WorkingDirectory = path;

            Process.Start(info);
        }

        /// <summary>
        /// Kills the process of service host
        /// </summary>
        public static void KillDummyService()
        {
            Process.GetProcessesByName("DummyService").ToList().ForEach(x => x.Kill());
        }
    }
}
