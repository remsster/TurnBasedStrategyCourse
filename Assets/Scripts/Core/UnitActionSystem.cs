using System;
using UnityEngine;
using UnityEngine.EventSystems;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.Core
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private bool isBusy;
        private BaseAction selectedAction;

        public Unit SelectedUnit => selectedUnit;
        public static UnitActionSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;
        public event EventHandler OnSelectedActionChanged;


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

        private void Start()
        {
            SetSelectedUnit(selectedUnit);
        }

        private void Update()
        {
            if (isBusy) return;
            // returns if mouse is over a ui button
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
            if (TryHandleUnitSelection()) { return; }   

            HandleSelectedAction();
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        private void HandleSelectedAction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy(true);
                    selectedAction.TakeAction(mouseGridPosition,SetBusy);
                }


                //switch (selectedAction)
                //{
                //    case MoveAction moveAction:
                //        if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                //        {
                //            SetBusy(true);
                //            selectedUnit.GetMoveAction().Move(mouseGridPosition, SetBusy);
                //        }
                //        break;
                //    case SpinAction spinAction:
                //        SetBusy(true);
                //        selectedUnit.GetSpinAction().Spin(SetBusy);
                //        break;
                //}
            }
        }

        private void SetBusy(bool value)
        {
            isBusy = value;
        }
        

        private bool TryHandleUnitSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
                {
                    if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                    {
                        if (unit == selectedUnit) return false;
                        SetSelectedUnit(unit);
                        return true;
                    }
                }
            }
            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            SetSelectedAction(unit.GetMoveAction());
            OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
        }

        public void SetSelectedAction(BaseAction baseAction)
        {
            selectedAction = baseAction;
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }

        public BaseAction GetSelectedAction() => selectedAction;
    }
}


