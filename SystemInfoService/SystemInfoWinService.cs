using System.ServiceModel;
using System.ServiceProcess;

namespace SystemInfoService
{
    public partial class SystemInfoWinService : ServiceBase
    {
        private ServiceHost host;

        public SystemInfoWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if(host != null)
            {
                host.Close();
                host = null;
            }

            host = new ServiceHost(typeof(SystemInfoService));

            host.Open();
        }

        protected override void OnStop()
        {
            host?.Close();
        }
    }
}
