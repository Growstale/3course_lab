using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace WebApplication1
{
    public class IISHandler : IHttpHandler
    {
        WebSocket socket;
        private static readonly string logFilePath = "D://5sem_BSTU//STRWP//lab2.2//WebApplication1//log.txt";

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(WebSocketRequest);
            }
        }
        private async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            socket = context.WebSocket;

            var receiveTask = ReceiveMessagesAsync();
            var sendTask = SendMessagesAsync();

            await Task.WhenAny(receiveTask, sendTask);

            if (socket.State == WebSocketState.Open || socket.State == WebSocketState.CloseReceived)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Закрытие соединения", CancellationToken.None);
                await LogMessage("Закрытие соединения");
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new ArraySegment<byte>(new byte[512]);

            while (socket.State == WebSocketState.Open)
            {
                var receiveResult = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                var clientMessage = System.Text.Encoding.UTF8.GetString(buffer.Array, 0, receiveResult.Count);
                await LogMessage("Сообщение от клиента: " + clientMessage);
            }
        }

        private async Task SendMessagesAsync()
        {
            while (socket.State == WebSocketState.Open)
            {
                if (socket.State != WebSocketState.Open)
                    break;

                await Send(DateTime.Now.ToString());
                await Task.Delay(2000); 
            }
        }

        private async Task Send(string s)
        {
            var message = "Ответ: " + s;
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);
            await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        private async Task LogMessage(string message)
        {
            var logEntry = $"{DateTime.Now}: {message}\n";

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                await writer.WriteLineAsync(logEntry);
            }
        }

    }
}