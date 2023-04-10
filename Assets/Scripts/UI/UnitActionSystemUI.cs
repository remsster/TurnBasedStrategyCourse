using System;

using UnityEngine;

using TurnBaseStrategy.Core;
using TurnBaseStrategy.Action;
using System.Collections.Generic;
using TMPro;

namespace TurnBaseStrategy.UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainerTransform;
        [SerializeField] private TextMeshProUGUI actionPointsText;

        private List<ActionButtonUI> actionButtonUIList;

        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }

        private void Start()
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void OnEnable()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStart += UnitActionSystem_OnActionStart;
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform transform in actionButtonContainerTransform)
            {
                Destroy(transform.gameObject);
            }

            actionButtonUIList.Clear();

            Unit unit = UnitActionSystem.Instance.SelectedUnit;
            foreach (BaseAction baseAction in unit.GetBaseActionArray())
            {
                Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
                ActionButtonUI b = actionButton.GetComponent<ActionButtonUI>();
                b.SetBaseAction(baseAction);
                actionButtonUIList.Add(b);
            }
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender,EventArgs e)
        {
            UpdateSelectedVisual();
        }    

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            CreateUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnActionStart(object sender, EventArgs e)
        {
            UpdateActionPoints();
        }

        private void UpdateSelectedVisual()
        {
            foreach(ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                actionButtonUI.UpdateSelectedVisual();
            }
        }

        private void UpdateActionPoints()
        {
            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
        }
    }
}
