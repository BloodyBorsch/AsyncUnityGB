using UnityEngine;
using System;


namespace LessonFour
{
    public sealed class GameManager : MonoBehaviour
    {
        public static event Action OnUpdateEvent;

        private void Update()
        {
            if (OnUpdateEvent != null) OnUpdateEvent?.Invoke();
        }

        public static void SubscribeOnUpdate(Action callback)
        {
            OnUpdateEvent += callback;
        }

        public static void UnSubscribeOnUpdate(Action callback)
        {
            OnUpdateEvent -= callback;
        }
    }
}