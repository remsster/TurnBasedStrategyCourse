using System;
using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int health = 100;

        public event EventHandler OnDead;

        private void Die()
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        public void Damage(int damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0);
            if (health == 0) Die();
        }

        
    }
}
