using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeRecieved(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

       Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
           Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        if (Server.clients.ContainsKey(_fromClient))
        {
            Server.clients[_fromClient].SendIntoGame(_username);
        }
    }

    public static void PlayerMovement(int _client, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }
        Quaternion _rotation = _packet.ReadQuaternion();

        if (Server.clients.ContainsKey(_client))
        {
            Server.clients[_client].player.SetInput(_inputs, _rotation);
        }
    }

    public static void PlayerShoot(int _client, Packet _packet)
    {
        Vector3 _shootDirection = _packet.ReadVector3();

        Server.clients[_client].player.Shoot(_shootDirection);
    }

    public static void StartGame(int client, Packet _packet)
    {
        if(Server.clients[client].player.isHost)
        {
            Debug.Log("Start Game!");
            ServerSend.GameStarted(1);
        }
    }
}
