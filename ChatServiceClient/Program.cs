using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using JimmikerNetwork;
using JimmikerNetwork.Client;
//using PacketType;

namespace ChatServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(
                    "Usage: ChatServiceClient [options...]\n" +
                    " --help   info for command\n" +
                    " -h   remote host\n" +
                    " -p   remote port\n"
                    );
                return;
            }
            string host = "";
            int port = -1;

            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i])
                {
                    case "-h":
                        {
                            i++;
                            host = args[i];
                            break;
                        }
                    case "-p":
                        {
                            i++;
                            port = Convert.ToInt32(args[i]);
                            break;
                        }
                    case "--help":
                        {
                            Console.WriteLine("list -h <remotehost> -p <remoteport> -d <remotepath>");
                            break;
                        }
                }
            }
            if(host == "" || port == -1)
            {
                Console.WriteLine("please add your remote host and port.");
                return;
            }


            Client client = new Client(ProtocolType.Tcp);
            client.Connect(host, port);

            /*SetConsoleCtrlHandler(t =>
            {
                Console.WriteLine("Closing...");
                client.clientLinker.Disconnect();
                SpinWait.SpinUntil(() => client.stop);
                Console.WriteLine("OK...");
                return false;
            }, true);*/

            SpinWait.SpinUntil(() => client.clientLinker.linkstate != LinkCobe.None);

            if (client.clientLinker.linkstate == LinkCobe.Connect)
            {
                for (string data = Console.ReadLine(); data != "exit"; data = Console.ReadLine())
                {
                    if (data == null)
                    {
                        break;
                    }
                    client.clientLinker.Ask(0, data);
                }
            }
            client.clientLinker.Disconnect();
            SpinWait.SpinUntil(() => client.stop);
        }

        /*[DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        public delegate bool ConsoleCtrlDelegate(CtrlTypes ctrlType);

        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }*/
    }
}
