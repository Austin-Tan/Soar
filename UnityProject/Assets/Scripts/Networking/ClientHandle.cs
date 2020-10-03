using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet) {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();
        
        Debug.Log($"Welcome message from server: {_msg}");
        Client.instance.myId = _myId;
        
        // TCP Welcome packet response
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint) Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet) {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP, message:\n{_msg}");
        ClientSend.UDPTestReceived();
    }
}
