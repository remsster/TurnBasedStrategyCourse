using System;
using UnityEngine;

using TurnBaseStrategy.Grid;

namespace TurnBaseStrategy.Core
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        public Unit SelectedUnit => selectedUnit;
        public static UnitActionSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one UnitActionSystem " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) { return; }
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    selectedUnit.GetMoveAction().Move(mouseGridPosition);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Should Spin");
                selectedUnit.GetSpinAction().Spin();
            }
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectedUnitChanged.Invoke(this,EventArgs.Empty);
        }

    }
}


