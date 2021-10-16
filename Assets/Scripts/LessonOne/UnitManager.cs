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
        private float _delayValue = 3.0f;
        private float _coolDown = 0.5f;

        private void Start()
        {
            _inputManager = new InputManager(this);
            _waitingFor = new WaitForSeconds(_coolDown);
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
            StartCoroutine(Healing());
        }

        IEnumerator Healing()
        {
            float time = _delayValue;

            while (time > 0)
            {
                time -= Time.deltaTime;

                if (!_currentUnit.CheckMaxHealth())
                {
                    _currentUnit.ChangeHealth(_healingValue);
                    yield return _waitingFor;
                }

                Debug.Log($"{time}");
            }
        }
    }
}