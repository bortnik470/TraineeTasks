using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoService
{
    public class SystemInfoService : ISystemInfoService
    {
        public string GetSystemInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Machine Name: ");
            sb.AppendLine(Environment.MachineName);
            sb.Append("User Name: ");
            sb.AppendLine(Environment.UserName);
            sb.Append("OS Version: ");
            sb.AppendLine(Environment.OSVersion.ToString());
            sb.Append("User Domain Name: ");
            sb.AppendLine(Environment.UserDomainName.ToString());

            return sb.ToString();
        }
    }
}
