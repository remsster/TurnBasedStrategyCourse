using System;

using UnityEngine;
using UnityEngine.UI;

using TurnBaseStrategy.Core;
using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        private void Start()
        {
            CreateUnitActionButtons();
        }

        private void OnEnable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform transform in actionButtonContainerTransform)
            {
                Destroy(transform.gameObject);
            }
            Unit unit = UnitActionSystem.Instance.SelectedUnit;
            foreach (BaseAction baseAction in unit.GetBaseActionArray())
            {
                Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                actionButton.GetComponent<ActionButtonUI>().SetBaseAction(baseAction);
            }
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
        }
    }
}
