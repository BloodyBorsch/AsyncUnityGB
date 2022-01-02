using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace LessonNine
{
    public class PopUpMenu : BaseMenuObject
    {
        private const string CONTINUE = "Continue";

        [SerializeField] private Button _continue;
        [SerializeField] private TMP_Text _text;

        public override void Awake()
        {
            base.Awake();
            _continue.GetComponentInChildren<TMP_Text>().SetText(CONTINUE);
            _continue.onClick.AddListener(Hide);
            _text.SetText("null");
        }

        public void SendText(string message)
        {
            _text.SetText(message);
        }
    }
}