using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingTemplate;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var chat = new Chat();
            chat.SendMessage("Ivan", "Hi");

        }
    }
}
