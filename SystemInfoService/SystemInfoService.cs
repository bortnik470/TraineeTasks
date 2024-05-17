using log4net;
using System;
using System.Reflection;
using System.Text;

namespace SystemInfoService
{
    public class SystemInfoService : ISystemInfoService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public string GetSystemInfo()
        {
            log.Debug("User call a SystemInfo method");
            StringBuilder sb = new StringBuilder();

            sb.Append("Machine Name: ");
            sb.AppendLine(Environment.MachineName);
            log.Debug($"Machine name {Environment.MachineName} had been appended to info");
            sb.Append("User Name: ");
            sb.AppendLine(Environment.UserName);
            log.Debug($"User Name {Environment.UserName} had been appended to info");
            sb.Append("OS Version: ");
            sb.AppendLine(Environment.OSVersion.ToString());
            log.Debug($"OS Version {Environment.OSVersion} had been appended to info");
            sb.Append("User Domain Name: ");
            sb.AppendLine(Environment.UserDomainName.ToString());
            log.Debug($"User Domain Name {Environment.UserDomainName} had been appended to info");

            log.Debug("Method return information");
            return sb.ToString();
        }
    }
}