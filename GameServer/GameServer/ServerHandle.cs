using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} ({_username}) received welcome packet and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"CRITICAL ERROR: Player \"{_username}\" ID: {_fromClient} has assumed the wrong client ID: {_clientIdCheck}");
            }
            
            // use TCP to send this client into game, tell them about everyone else, and tell everyone else about them.
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void UDPTestReceived(int _fromClient, Packet _packet)
        {
            string _msg = _packet.ReadString();
            Console.WriteLine($"Received packet via UDP from client {_fromClient}, message:\n{_msg}");
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            // length of inputs array is the first of this packet
            bool[] _inputs = new bool[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
            Console.WriteLine($"Received player {_fromClient}'s movement update: {_inputs}");
        }
    }
}
