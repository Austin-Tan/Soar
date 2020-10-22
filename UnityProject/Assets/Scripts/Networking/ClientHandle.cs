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
    
    public static void PlayerPosition(Packet _packet) {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        // Debug.Log($"Update from player {_id} on position: {_position}");
        if (!GameManager.players.ContainsKey(_id)) {
            Debug.LogError($"Erroneously received position packet for player {_id} who does not exist.");
            return;
        }
        GameManager.players[_id].transform.position = _position;
    }

    public static void UDPTest(Packet _packet) {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP, message:\n{_msg}");
        ClientSend.UDPTestReceived();
    }

    public static void SpawnPlayer(Packet _packet) {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        Debug.Log($"SpawnPlayer packet received, now spawning player {_id}.");
        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void RemovePlayer(Packet _packet) {
        int _id = _packet.ReadInt();

        GameManager.instance.RemovePlayer(_id);
    }
}
