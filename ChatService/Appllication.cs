using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using JimmikerNetwork;
using JimmikerNetwork.Server;
using System.IO;

namespace ChatService
{
    public class Appllication : AppllicationBase
    {
        public event Action<MessageType, string> GetMessage;

        public List<Peer> allpeers { get; private set; }

        public Appllication(ProtocolType protocol):base(IPAddress.IPv6Any, 6888, protocol)
        {
        }

        protected override void Setup()
        {
            allpeers = new List<Peer>();
            RunUpdateThread();
        }

        protected override PeerBase AddPeerBase(object _peer, INetServer server)
        {
            Peer peer = new Peer(_peer, server, this);
            allpeers.Add(peer);
            return peer;
        }

        protected override void TearDown()
        {
            
        }

        protected override void DebugReturn(MessageType messageType, string msg)
        {
            GetMessage?.Invoke(messageType, msg);
        }
    }
}