using System;
using UnityEngine;
using UnityEngine.UI;


namespace LessonNine
{
    public class TheCard : MonoBehaviour, IDisposable
    {
        public Button ActivateButton => _activateButton;

        private TimeManager _production;
        private Image _image;
        private Button _activateButton;

        private float _productionTime = 0;
        private float _tempTimer;

        private bool _isReady = true;

        private void Awake()
        {            
            _image = GetComponent<Image>();
            _image.fillAmount = 0;
            _activateButton = GetComponent<Button>();
            _production = new TimeManager(StartProduction, false);
        }

        private void Start()
        {
            MainSceneController.SubscribeToUpdate(StartCounter);
        }

        public void GetProperties(CardProperties properties)
        {
            _isReady = false;
            _image.fillAmount = 0;
            _image.sprite = properties.Image;
            _productionTime = properties.ProductionTime;
            _tempTimer = 0;
            _production.UseAfterCoolDown(_productionTime);
        }

        public void StartProduction()
        {            
            _isReady = true;
        }

        public void StartCounter()
        {
            if (_productionTime <= 0) return;

            if (_image.fillAmount < 1)
            {
                _tempTimer += Time.deltaTime;
                var percent = _tempTimer / _productionTime;
                _image.fillAmount = percent;
            }
        }

        public bool ReadyCheck()
        {
            return _isReady;
        }

        public void Dispose()
        {
            MainSceneController.UnsubscribeFromUpdate(StartCounter);
        }
    }
}