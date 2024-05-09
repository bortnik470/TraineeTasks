using System.ServiceModel;

namespace SystemInfoService
{
    [ServiceContract]
    public interface ISystemInfoService
    {
        [OperationContract]
        string GetSystemInfo();
    }
}
