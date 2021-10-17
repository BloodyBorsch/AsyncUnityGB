using UnityEngine;


namespace AsyncUnityGB
{
    public sealed class Unit : MonoBehaviour
    {
        private int _health;
        private int _maxHealth = 100;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void ChangeHealth(int value)
        {
            _health += value;
            Debug.Log($"Изменен параметр Health: {_health}");
        }

        public bool CheckMaxHealth()
        {
            if (_health > _maxHealth)
            {
                _health = _maxHealth;                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}