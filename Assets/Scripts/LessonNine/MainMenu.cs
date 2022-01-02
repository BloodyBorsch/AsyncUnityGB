using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace LessonNine
{
    public class MainMenu : BaseMenuObject
    {
        private const string SHOP_MENU_NAME = "Shop Menu";

        [SerializeField] private Button _shopMenu;

        public Button OpenShopMenu => _shopMenu;

        public override void Awake()
        {
            base.Awake();
            _shopMenu.GetComponentInChildren<TMP_Text>().text = SHOP_MENU_NAME;
        }
    }
}