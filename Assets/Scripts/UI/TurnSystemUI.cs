using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.UI
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
        [SerializeField] private TextMeshProUGUI turnNumberText;
        [SerializeField] private GameObject enemyTurnVisual;

        private void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

            endTurnButton.onClick.AddListener(() => {
                TurnSystem.Instance.NextTurn();
            });

            UpdateTurnText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();
        }

        

        private void UpdateTurnText()
        {
            turnNumberText.text = "TURN " + TurnSystem.Instance.TurnNumber;
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnText();
            UpdateEnemyTurnVisual();
            UpdateEndTurnButtonVisibility();
        }

        private void UpdateEnemyTurnVisual()
        {
            enemyTurnVisual.SetActive(!TurnSystem.Instance.IsPlayerTurn);
        }

        private void UpdateEndTurnButtonVisibility()
        {
            endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn);
        }

    }

}
