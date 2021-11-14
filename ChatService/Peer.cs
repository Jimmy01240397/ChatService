using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using JimmikerNetwork;
using JimmikerNetwork.Server;
//using PacketType;

namespace ChatService
{
    public class Peer : PeerBase
    {

        Appllication appllication;
        public Peer(object peer, INetServer server, Appllication _appllication) : base(peer, server)
        {
            //Console.WriteLine("aa");
            appllication = _appllication;
        }

        public override void OnOperationRequest(SendData sendData)
        {
            switch(sendData.Code)
            {
                case 0:
                    {
                        Console.WriteLine(RemoteEndPoint.ToString() + ": " + sendData.Parameters.ToString());
                        foreach (Peer a in appllication.allpeers)
                        {
                            if(a != this)
                            {
                                a.Tell(0, RemoteEndPoint.ToString() + ": " + sendData.Parameters.ToString());
                            }
                        }
                        break;
                    }
            }
        }

        public override void OnDisconnect()
        {
            appllication.allpeers.Remove(this);
        }
    }
}