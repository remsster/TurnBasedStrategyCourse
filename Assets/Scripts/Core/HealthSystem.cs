using System;
using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private int health = 100;
        private int healthMax;

        public event EventHandler OnDead;
        public event EventHandler OnDamaged;

        private void Awake()
        {
            healthMax = health;
        }

        private void Die()
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        public void Damage(int damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0);
            if (health == 0) Die();
            OnDamaged?.Invoke(this, EventArgs.Empty);
        }

        public float GetNormalizedHealth()
        {
            return (float)health / healthMax;
        }

        
    }
}
