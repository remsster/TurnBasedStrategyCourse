using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.UI
{

    public class UnitWorldUI : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI actionPointText;
        [SerializeField] private Unit unit;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private HealthSystem healthSystem;

        private void Start()
        {
            Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;
            healthSystem.OnDamaged += HealthSystem_OnDamaged;
            UpdateActionPointsText();
            UpdateHealthBar();
        }


        private void UpdateActionPointsText()
        {
            actionPointText.text = unit.GetActionPoints().ToString();
        }

        private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
        {
            UpdateActionPointsText();
        }

        private void HealthSystem_OnDamaged(object sneder, EventArgs e)
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthBarImage.fillAmount = healthSystem.GetNormalizedHealth();
        }

    }

}
