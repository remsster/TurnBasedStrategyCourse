using System;
using UnityEngine;

using TurnBaseStrategy.Grid;

namespace TurnBaseStrategy.Core
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private bool isBusy;

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
            if (isBusy) return;
            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) { return; }
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy(true);
                    selectedUnit.GetMoveAction().Move(mouseGridPosition, SetBusy);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                SetBusy(true);
                selectedUnit.GetSpinAction().Spin(SetBusy);
            }
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        private void SetBusy(bool value)
        {
            isBusy = value;
        }
        

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


