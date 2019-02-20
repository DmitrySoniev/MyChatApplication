using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;

namespace TestingTemplate
{
    public class Chat
    {
        public List<Message> History;
        private readonly HubConnection connection;
        private bool _connect;

        public Chat()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.62:50338/chat")
                .Build();
            connection.On<string, string>("ReceiveMessage", (user, message) => UpdateMessage(user, message));

            History = new List<Message>();
            UpdateMessage += ChatUpdateMessage;
        }

        public delegate void UpdateHandlerHistory(List<Message> history);

        private delegate void UpdateHandler(string user, string message);

        public event UpdateHandlerHistory UpdateHistory;

        private event UpdateHandler UpdateMessage;

        public Status Login(User user)
        {
            throw new NotImplementedException();
            return new Status {Success = true};
        }

        public Status Registration(RegisterData data)
        {
            throw new NotImplementedException();
            return new Status {Success = true};
        }

        public Status SendMessage(string user, string message)
        {
            //if (_connect)
            //{
            //throw new NotImplementedException();

            //user = "Ivan";
            //message = "Hi";
            //UpdateMessage(user, message);7
            throw new NotImplementedException();

            //return new Status { Success = true };
            //}
            //else
            //{
            //    return new Status { Success = false, ErrorMessage = "Нет подключения" };
            //}
        }

        private void ChatUpdateMessage(string user, string message)
        {
            History.Add(new Message {User = user, TextMessage = message});
            UpdateHistory(History);
        }
    }
}