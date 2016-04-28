using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatServer
{
    class Program
    {
        private void RunServer()
        {

            using (ServiceHost host = new ServiceHost(typeof(WcfChatPost.ChatService)))
            {
                host.Open();

                Console.WriteLine("qwert");
                Console.WriteLine("fdsgdsgdsgdsgdsgdsgdsg");
                Console.ReadLine();
                host.Close();
            }
        }
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.RunServer();
        }

        
    }
}
