using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LessonThree
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _startServerButton;
        [SerializeField] private Button _shutDownServerButton;
        [SerializeField] private Button _connectClientButton;
        [SerializeField] private Button _disconnectClientButton;
        [SerializeField] private Button _sendMessageButton;

        [SerializeField] private TMP_InputField _msgInputField;

        [SerializeField] private ChatFieldUI _textField;

        [SerializeField] private Server _server;
        [SerializeField] private Client _client;

        private void Start()
        {
            _textField.Initialize();
            _startServerButton.onClick.AddListener(() => StartServer());
            _shutDownServerButton.onClick.AddListener(() => ShutDownServer());
            _connectClientButton.onClick.AddListener(() => Connect());
            _disconnectClientButton.onClick.AddListener(() => Disconnect());
            _sendMessageButton.onClick.AddListener(() => SendMessage());
            _client.OnRecieveMSG += RecieveMessage;
        }

        private void Update()
        {
            if (_msgInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage();
            }
        }

        private void StartServer()
        {
            _server.StartServer();
        }

        private void ShutDownServer()
        {
            _server.ShutDownServer();
        }

        private void Connect()
        {
            _client.Connect(_server.GetHostID());
            _msgInputField.ActivateInputField();
        }

        private void Disconnect()
        {
            _client.Disconnect();
        }

        private void SendMessage()
        {
            _client.SendMessage(_msgInputField.text);
            //_server.SendMessage(_msgInputField.text, _client);
            _msgInputField.text = "";
            _msgInputField.ActivateInputField();
        }

        private void RecieveMessage(string message)
        {
            _textField.RecieveMessage(message);
        }
    }
}