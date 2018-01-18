using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceModel.Web;

namespace SelfSignalRSvc
{
    public class Program
    {
        private static string ServerUri = "http://192.165.1.162:118"; // SignalR服务地址

        public static void Main(string[] args)
        {
            WebServiceHost serviceHost = new WebServiceHost();
            try
            {
                IDisposable SignalR = WebApp.Start(ServerUri);  // 启动SignalR服务
                Console.WriteLine("***发布地址：" + ServerUri + "***");
                Console.WriteLine("**********启动服务成功**********");
                Console.ReadKey();
                serviceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("!!!!!!!!!!启动服务失败!!!!!!!!!!");
                Console.Write(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
