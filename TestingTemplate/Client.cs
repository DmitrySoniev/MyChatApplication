using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TestingTemplate

{
    /// <summary>
    /// Обеспечивает простой интерфейс для соедения с чат сервером
    /// </summary>
    public class Client
    {
        public Action<string, string> ReceiveMessage;
        private readonly string _server;

        private HubConnection _connection;

        private string _token;

        public Client(string serverAddress)
        {
            _server = serverAddress;
        }

        public enum Gender
        {
            Men,
            Women
        }

        public List<string> GetAllUsers()
        {
            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Без токена не возможно соединение с сервером!");
            }
            var client = new RestClient(_server + "/Account/GetAllUsers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "bearer " + _token);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<string>>(response.Content);
        }

        /// <summary>
        /// Аунтефикация
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public void Login(string email, string password)
        {
            var client = new RestClient(_server + "/auth/gettoken");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            byte[] bytes = Encoding.ASCII.GetBytes(email + ":" + password);
            var basic64 = Convert.ToBase64String(bytes);
            request.AddHeader("Authorization", "Basic " + basic64);

            IRestResponse response = client.Execute(request);

            _token = response.Content;
        }

        public void Register(string email, string password, string firstname, string lastname, Gender gender)
        {
            var client = new RestClient(_server + "/Account/Register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "8fdd4a66-45fd-4308-80c1-896607c3c4b1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            var r = new RegisterModel();
            r.Email = email;
            r.Password = password;
            r.FirstName = firstname;
            r.LastName = lastname;
            switch (gender)
            {
                case Gender.Men:
                    r.Gender = "M";
                    break;

                case Gender.Women:
                    r.Gender = "W";
                    break;
            }
            string body = JsonConvert.SerializeObject(r);
            request.AddParameter("undefined", body, ParameterType.RequestBody);
            //request.AddParameter("undefined", $"{{\"FirstName\":{FirstName},\n\"LastName\":{LastName},\n\"Gender\":{Gender},\n\"Email\":{Email},\n\"Password\":{Password}\n}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.Content != "Успешно")
            {
                throw new Exception(response.Content);
            }
        }

        public void SendMessage(string email, string message)
        {
            _connection.SendAsync("SendMessage", email, message);
        }

        /// <summary>
        /// Запуск чата
        /// </summary>
        public void StartChat()
        {
            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Без токена не возможно соединение с сервером!");
            }
            _connection = new HubConnectionBuilder()
               .WithUrl(_server + "/chat",
                   options => { options.AccessTokenProvider = () => Task.FromResult(_token); })
               .Build();

            _connection.On<string, string>("ReceiveMessage", ReceiveMessage);

            _connection.StartAsync();
        }

        private class RegisterModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [Required]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        }
    }
}