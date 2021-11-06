using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;


namespace LessonThree
{
    public sealed class Server : MonoBehaviour, INetworkUser
    {
        public delegate void OnMessageRecieve(string message);
        public event OnMessageRecieve OnRecieveMSG;

        private const string START_SERVER_MESSAGE = "---Server Online---";
        private const string SHUTDOWN_SERVER_MESSAGE = "---Server Offline---";        

        private const int MAX_CONNECTIONS = 10;
        private int _port = 5805;
        private int _hostID;
        private int _reliableChannel;

        private bool _isStarted = false;
        private byte _error;

        List<int> _connectionIDs;
        Dictionary<int, string> _users;

        private void Update()
        {
            if (!_isStarted) return;

            int recHostId;
            int connectionId;
            int channelId;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;

            NetworkEventType recData = NetworkTransport.Receive(out recHostId,
                                                                out connectionId,
                                                                out channelId,
                                                                recBuffer,
                                                                bufferSize,
                                                                out dataSize,
                                                                out _error);

            while (recData != NetworkEventType.Nothing)
            {
                switch (recData)
                {
                    case NetworkEventType.Nothing:
                        break;

                    case NetworkEventType.ConnectEvent:
                        _connectionIDs.Add(connectionId);
                        Debug.Log($"Player {connectionId} has connected.");
                        break;

                    case NetworkEventType.DataEvent:
                        string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);

                        string playerName;

                        if (_users.TryGetValue(connectionId, out playerName))
                        {
                            SendMessageToAll($"{playerName}: {message}");
                        }
                        else
                        {
                            _users.Add(connectionId, message);
                            SendMessageToAll($"Player {message} has connected.");
                        }                        
                        break;

                    case NetworkEventType.DisconnectEvent:   
                        string disconnectedPlayer;            
                        
                        _users.TryGetValue(connectionId, out disconnectedPlayer);
                        SendMessageToAll($"Player {disconnectedPlayer} has disconnected");
                        Debug.Log($"Player {disconnectedPlayer} with id {connectionId} has disconnected.");
                        _users.Remove(connectionId);
                        _connectionIDs.Remove(connectionId);
                        break;

                    case NetworkEventType.BroadcastEvent:
                        break;
                }

                recData = NetworkTransport.Receive(out recHostId,
                                                   out connectionId,
                                                   out channelId,
                                                   recBuffer,
                                                   bufferSize,
                                                   out dataSize,
                                                   out _error);
            }
        }

        public void StartServer()
        {
            _connectionIDs = new List<int>();
            _users = new Dictionary<int, string>();

            NetworkTransport.Init();
            ConnectionConfig cc = new ConnectionConfig();
            _reliableChannel = cc.AddChannel(QosType.Reliable);
            HostTopology topology = new HostTopology(cc, MAX_CONNECTIONS);
            _hostID = NetworkTransport.AddHost(topology, _port);

            _isStarted = true;
            Debug.Log($"{START_SERVER_MESSAGE}");
        }
                
        public void ShutDownServer()
        {
            if (!_isStarted) return;

            _connectionIDs.Clear();
            _users.Clear();
            NetworkTransport.RemoveHost(_hostID);
            NetworkTransport.Shutdown();

            _isStarted = false;

            Debug.Log($"{SHUTDOWN_SERVER_MESSAGE}");
        }

        public void SendMessage(string message, int connectionID)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);
            NetworkTransport.Send(_hostID, connectionID, _reliableChannel,
                buffer, message.Length * sizeof(char), out _error);
            if ((NetworkError)_error != NetworkError.Ok) Debug.Log((NetworkError)_error);
        }

        public void SendMessageToAll(string message)
        {
            if (_connectionIDs.Count != 0)
            {
                foreach (var connectionid in _connectionIDs)
                {
                    SendMessage(message, connectionid);
                    OnRecieveMSG?.Invoke(message);
                }
            }
            else
            {
                Debug.Log($"No Clients =(");
            }
        }
    }
}