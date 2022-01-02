using System;
using UnityEngine;


namespace LessonNine
{
    public class TimeManager : IDisposable
    {        

        private enum StateOfManager
        {
            None = 0,
            Waiting = 1,
            AfterCD = 2,
            BeforeCD = 3
        }

        private Action _method;
        private StateOfManager _stateOfManager;

        private float _timeCD;
        private float _currentTime;

        private bool _isRepeating;

        public TimeManager(Action method, bool isRepeating = true)
        {
            _method += method;
            _isRepeating = isRepeating;
            _stateOfManager = StateOfManager.Waiting;

            MainSceneController.SubscribeToUpdate(Execute);
        }

        public void SetRepeating(bool repeat)
        {
            _isRepeating = repeat;
        }

        public void UseAfterCoolDown(float time)
        {
            _timeCD = time;
            _currentTime = _timeCD;
            _stateOfManager = StateOfManager.AfterCD;
        }

        public void UseBeforeCoolDown(float time)
        {
            _timeCD = time;
            _currentTime = 0;
            _stateOfManager = StateOfManager.BeforeCD;
        }

        public void RemoveCoolDown()
        {
            _stateOfManager = StateOfManager.Waiting;
        }

        private void Execute()
        {
            if (_stateOfManager != StateOfManager.Waiting)
            {
                if (_stateOfManager == StateOfManager.AfterCD)
                {
                    UseAfterCD();
                }

                if (_stateOfManager == StateOfManager.BeforeCD)
                {
                    UseBeforeCD();
                }
            }
            else
            {
            }
        }

        private void UseBeforeCD()
        {
            if (_currentTime >= 0.0f)
            {
                _currentTime -= Time.deltaTime;
            }
            else
            {
                _method?.Invoke();

                if (!_isRepeating)
                {
                    RemoveCoolDown();
                }
                else
                {
                    _currentTime = _timeCD;
                }
            }
        }

        private void UseAfterCD()
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0.0f)
            {
                _method?.Invoke();

                if (!_isRepeating)
                {
                    RemoveCoolDown();
                }
                else
                {
                    _currentTime = _timeCD;
                }
            }
        }

        public void Dispose()
        {
            MainSceneController.UnsubscribeFromUpdate(Execute);
            _method = null;
        }
    }
}