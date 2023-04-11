using System;

using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class TurnSystem : MonoBehaviour
    {
        private int turnNumber = 1;
        private bool isPlayerTurn = true;

        public bool IsPlayerTurn => isPlayerTurn;

        public int TurnNumber => turnNumber;

        public event EventHandler OnTurnChanged;

        public static TurnSystem Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one TurnSystem " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void NextTurn()
        {
            turnNumber++;
            isPlayerTurn = !isPlayerTurn;
            OnTurnChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
