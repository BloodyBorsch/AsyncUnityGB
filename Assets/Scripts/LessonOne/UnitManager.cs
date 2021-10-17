using System;
using System.Collections;
using UnityEngine;


namespace AsyncUnityGB
{
    public sealed class UnitManager : MonoBehaviour
    {
        public Action OnUpdate;

        private Unit _currentUnit;
        private InputManager _inputManager;
        private WaitForSeconds _waitingFor;

        private int _damageValue = 10;
        private int _healingValue = 5;
        private float _delayValue;
        private float _maxDelayValue = 3.0f;
        private float _coolDown = 0.5f;

        private bool _buffed = false;

        private void Start()
        {
            _delayValue = _maxDelayValue;
            _inputManager = new InputManager(this);
            _waitingFor = new WaitForSeconds(_coolDown);
            OnUpdate += TimerChange;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnUpdate = null;
        }

        public void GetUnit(Unit current)
        {
            _currentUnit = current;
        }

        public void DoDamage()
        {
            _currentUnit.ChangeHealth(-_damageValue);
        }

        public void RecieveHealing()
        {
            if (!_buffed)
            {
                StartCoroutine(Healing());
            }
        }

        private void TimerChange()
        {
            if (_buffed)
            {                
                _delayValue -= Time.deltaTime;
            }
        }

        private IEnumerator Healing()
        {            
            _buffed = true;

            while (_delayValue > 0)
            {        
                if (!_currentUnit.CheckMaxHealth())
                {
                    _currentUnit.ChangeHealth(_healingValue);
                    yield return _waitingFor;
                }
            }
            
            _buffed = false;
            _delayValue = _maxDelayValue;
        }
    }
}