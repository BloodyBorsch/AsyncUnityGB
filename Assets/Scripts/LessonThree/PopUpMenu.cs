using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace LessonThree
{
    public sealed class PopUpMenu : MonoBehaviour
    {
        private TMP_InputField _inputField;

        public void Initialize()
        {
            _inputField = GetComponentInChildren<TMP_InputField>(true);
        }

        public void TypeName(Client client)
        {
            client.GetName(_inputField.text);
            _inputField.text = "";
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
            _inputField.ActivateInputField();            
        }

        public void CloseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}