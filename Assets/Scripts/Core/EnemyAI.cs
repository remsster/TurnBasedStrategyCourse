using System;

using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class EnemyAI : MonoBehaviour
    {
        private float timer;

        private void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnchanged;
        }

        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn) return;
            timer -= Time.deltaTime;
            if  (timer <= 0)
            {
                TurnSystem.Instance.NextTurn();
            }
        }

        private void TurnSystem_OnTurnchanged(object sender, EventArgs e)
        {
            timer = 2f;
        }
    }
}
