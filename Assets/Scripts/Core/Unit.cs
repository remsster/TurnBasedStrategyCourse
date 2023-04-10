using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {
        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;
        private int actionPoints = 2;

        public GridPosition GridPosition => gridPosition;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
            baseActionArray = GetComponents<BaseAction>();
            
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        private void Update()
        {

            

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }

        // ----------------------------------------------------------------------------
        // Overridden Methods
        // ----------------------------------------------------------------------------

        public override string ToString()
        {
            return gameObject.name;
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        // -- Private --
        private void SpendActionPoints(int amount)
        {
            actionPoints -= amount;
        }

        // -- Public --
        public SpinAction GetSpinAction() => spinAction;

        public MoveAction GetMoveAction() => moveAction;

        public BaseAction[] GetBaseActionArray() => baseActionArray;

        public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
        {
            if (CanSpendActionPointsToTakeAction(baseAction))
            {
                SpendActionPoints(baseAction.GetActionPointsCost());
                return true;
            }
            return false;

        }

        public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
        {
            return actionPoints >= baseAction.GetActionPointsCost();
        }

        public int GetActionPoints() => actionPoints;



        


        
    }
}

