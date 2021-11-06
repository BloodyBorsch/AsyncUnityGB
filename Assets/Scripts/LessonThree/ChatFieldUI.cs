using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LessonThree
{
    public class ChatFieldUI : MonoBehaviour
    {
        private TextMeshProUGUI _textObject;
        private Scrollbar _scrollbar;

        private List<string> _messages;

        public void Initialize()
        {
            _textObject = GetComponentInChildren<TextMeshProUGUI>();
            _scrollbar = GetComponentInChildren<Scrollbar>();
            _messages = new List<string>();
            _scrollbar.onValueChanged.AddListener((float value) => UpdateText());
            _textObject.text = "";
        }

        public void RecieveMessage(string message)
        {
            _messages.Add(message.ToString());
            float value = (_messages.Count - 1) * _scrollbar.value;
            _scrollbar.value = Mathf.Clamp(value, 0,1);
            UpdateText();
        }

        private void UpdateText()
        {
            string text = "";
            int index = (int)(_messages.Count * _scrollbar.value);

            for (int i = index; i < _messages.Count; i++)
            {
                text += _messages[i] + "\n";
            }

            _textObject.text = text;
        }
    }
}