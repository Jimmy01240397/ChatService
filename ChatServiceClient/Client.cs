using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.IO;

using JimmikerNetwork;
using JimmikerNetwork.Client;
//using PacketType;

namespace ChatServiceClient
{
    public class Client : ClientListen
    {
        public bool stop { get; set; } = false;
        public bool finish { get; set; } = false;

        public string downloadpath { get; set; } = "";

        public ClientLinker clientLinker { get; private set; }
        public Client(ProtocolType protocol)
        {
            clientLinker = new ClientLinker(this, protocol);
        }

        public bool Connect(string host, int port)
        {
            bool on = clientLinker.Connect(host, port);
            clientLinker.RunUpdateThread();
            return on;
        }

        public void DebugReturn(string message)
        {
            
        }

        public void OnEvent(SendData sendData)
        {
            switch(sendData.Code)
            {
                case 0:
                    {
                        Console.WriteLine(sendData.Parameters.ToString());
                        break;
                    }
            }
        }

        public void OnOperationResponse(SendData sendData)
        {
        }

        public void OnStatusChanged(LinkCobe connect)
        {
            switch (connect)
            {
                case LinkCobe.Connect:
                    {
                        //Console.WriteLine("connect Success");
                        break;
                    }
                case LinkCobe.Failed:
                    {
                        //Console.WriteLine("connect Failed");
                        clientLinker.StopUpdateThread();
                        stop = true;
                        break;
                    }
                case LinkCobe.Lost:
                    {
                        //Console.WriteLine("connect Lost");
                        clientLinker.StopUpdateThread();
                        stop = true;
                        break;
                    }
            }
        }

        public PeerForP2PBase P2PAddPeer(object _peer, object publicIP, INetClient client, bool NAT)
        {
            throw new NotImplementedException();
        }
    }
}