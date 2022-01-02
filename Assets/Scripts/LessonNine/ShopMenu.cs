using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


namespace LessonNine
{
    public class ShopMenu : BaseMenuObject
    {
        private const string CLOSE_MENU = "Close Menu";

        [SerializeField] private Button _closeMenu;
        [SerializeField] private CardHolder _cardHolder;
        private List<TheCard> _cards;

        private bool _isActivated;

        public Button CloseMenu => _closeMenu;

        public override void Awake()
        {
            base.Awake();
            _closeMenu.GetComponentInChildren<TMP_Text>().text = CLOSE_MENU;
            _cards = new List<TheCard>();
            _cards.AddRange(GetComponentsInChildren<TheCard>());
            _isActivated = false;
        }

        public void Activate()
        {
            CreateCards();

            _isActivated = true;
        }

        private void CreateCards()
        {
            if (_isActivated) return;

            foreach (var card in _cards)
            {
                GetCardValues(card);
                card.ActivateButton.onClick.AddListener(delegate { GetCardValues(card); });
            }
        }

        private void GetCardValues(TheCard card)
        {
            if (card.ReadyCheck() != true) return;

            card.GetProperties(_cardHolder.Cards.Skip(Random.Range(0, _cardHolder.Cards.Count())).FirstOrDefault());
        }
    }
}