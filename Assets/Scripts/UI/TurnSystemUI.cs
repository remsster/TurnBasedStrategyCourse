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

        private void Start()
        {
            endTurnButton.onClick.AddListener(() => {
                TurnSystem.Instance.NextTurn();
            });
            UpdateTurnText();
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        

        private void UpdateTurnText()
        {
            turnNumberText.text = "TURN " + TurnSystem.Instance.TurnNumber;
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            UpdateTurnText();
        }

    }

}
