using ServiceReference1;

SystemInfoServiceClient serviceClient = new SystemInfoServiceClient();

var info = serviceClient.GetSystemInfo();

Console.WriteLine(info);