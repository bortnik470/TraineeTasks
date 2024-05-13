using ServiceReference1;
using ServiceTest;

SystemInfoServiceClient serviceClient = new SystemInfoServiceClient();

var info = serviceClient.GetSystemInfo();

Console.WriteLine(info);