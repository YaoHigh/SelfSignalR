using Microsoft.AspNet.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SelfSignalRSvc
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            var name = Context.ConnectionId;
            // 调用所有客户端的sendMessage方法
            Clients.All.sendMessage(name, message);
        }

        /// <summary>
        /// 客户端连接的时候调用
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Trace.WriteLine("客户端连接成功:" + Context.ConnectionId);
            return base.OnConnected();
        }
    }
}