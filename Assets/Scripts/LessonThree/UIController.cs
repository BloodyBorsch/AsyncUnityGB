using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LessonThree
{
    public sealed class UIController : MonoBehaviour
    {
        [SerializeField] private Button _startServerButton;
        [SerializeField] private Button _shutDownServerButton;
        [SerializeField] private Button _connectClientButton;
        [SerializeField] private Button _disconnectClientButton;
        [SerializeField] private Button _sendMessageButton;
        [SerializeField] private Button _closeApplication;

        [SerializeField] private TMP_InputField _msgInputField;
        [SerializeField] private PopUpMenuLessonThree _nameMenu;
        [SerializeField] private ChatFieldUI _textField;

        [SerializeField] private Server _server;
        [SerializeField] private Client _client;

        private void Start()
        {
            _msgInputField.interactable = false;
            _textField.Initialize();
            _nameMenu.Initialize();
            _startServerButton.onClick.AddListener(() => StartServer());
            _shutDownServerButton.onClick.AddListener(() => ShutDownServer());
            _connectClientButton.onClick.AddListener(() => Connect());
            _disconnectClientButton.onClick.AddListener(() => Disconnect());            
            _closeApplication.onClick.AddListener(() => Close());
        }

        private void Update()
        {
            if (_msgInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Return))
            {
                _sendMessageButton.onClick.Invoke();
            }

            if (_nameMenu.isActiveAndEnabled && Input.GetKeyDown(KeyCode.Return))
            {
                TypeName();
            }
        }

        private void StartServer()
        {
            _server.OnRecieveMSG += RecieveMessage;
            _server.StartServer();
            _connectClientButton.interactable = false;
            _disconnectClientButton.interactable = false;
            _msgInputField.interactable = true;
            _msgInputField.ActivateInputField();
            _sendMessageButton.onClick.AddListener(() => SendMessage(_server));
        }

        private void ShutDownServer()
        {
            _server.ShutDownServer();
        }

        private void Connect()
        {
            _client.OnRecieveMSG += RecieveMessage;
            _client.Connect();
            _startServerButton.interactable = false;
            _shutDownServerButton.interactable = false;
            _sendMessageButton.onClick.AddListener(() => SendMessage(_client));
            _sendMessageButton.interactable = false;
            _nameMenu.ShowMenu();
        }

        private void Disconnect()
        {
            _client.Disconnect();
        }

        private void SendMessage(INetworkUser user)
        {
            if (_msgInputField.text.Length > 0) user.SendMessageToAll(_msgInputField.text);
            _msgInputField.text = "";
            _msgInputField.ActivateInputField();
        }
        
        private void TypeName()
        {
            _nameMenu.TypeName(_client);
            _nameMenu.CloseMenu();
            _sendMessageButton.interactable = true;
            _msgInputField.interactable = true;
            _msgInputField.ActivateInputField();
        }

        private void RecieveMessage(string message)
        {
            _textField.RecieveMessage(message);
        }

        private void Close()
        {
            _client.OnRecieveMSG -= RecieveMessage;
            _server.OnRecieveMSG -= RecieveMessage;
            Application.Quit();
        }
    }
}