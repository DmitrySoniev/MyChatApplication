using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestingTemplate;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static Chat chat;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            chat = new Chat();
        }

        [TestMethod]
        public void SendMessage()
        {
            const string User = "Ivan";
            const string Message = "Hi";
            var res = chat.SendMessage(User, Message);

            Assert.IsTrue(res.Success, res.ErrorMessage);
            Assert.AreEqual(Message, chat.History.Last().TextMessage);
            Assert.AreEqual(User, chat.History.Last().User);
        }
        
        [TestMethod]
        public void Register()
        {
            var data = new RegisterData()
            {
                Login = "Ivan",
                Password = "123456",
                Name = "Ivan",
                Surname = "Ivanov",
                Gender = "man"
            };
            var res = chat.Registration(data);

            Assert.IsTrue(res.Success, res.ErrorMessage);
        }

        [TestMethod]
        public void Login()
        {
			var user = new User()
            {
                Login = "Ivan",
                Password = "123456"
            };
            var res = chat.Login(user);

            Assert.IsTrue(res.Success, res.ErrorMessage);
        }
        
        [TestMethod]
        public async Task UpdateAsync()
        {
            //var user = new User()
            //{
            //    Login = "Ivan",
            //    Password = "123456"

            //};
            //var res = chat.Login(user);
            //Assert.IsTrue(res.Success, res.ErrorMessage);

            var sw = Stopwatch.StartNew();
            int countMessage = 0;
            chat.UpdateHistory += delegate (List<Message> history)
            {
                foreach (var message in history)
                {
                    Console.WriteLine($"{message.User}\t{message.TextMessage}");
                    countMessage++;
                }
            };
            const int ContSendMessage = 5;
            for (int i = 0; i < ContSendMessage; i++)
            {
                chat.SendMessage("Ivan", $"Hi{i}");
            }

            var timeOut = TimeSpan.FromSeconds(5);
            while (sw.Elapsed < timeOut)
            {
                await Task.Delay(1);
            }

            Assert.AreNotEqual(0, countMessage);
            Assert.AreEqual(5, countMessage);
        }
    }
}