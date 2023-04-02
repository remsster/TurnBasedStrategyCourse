using TurnBaseStrategy.Grid;
using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {
        private GridPosition gridPosition;
        private MoveAction moveAction;

        public GridPosition GridPosition => gridPosition;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            
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
        // Custom Methods
        // ----------------------------------------------------------------------------

        public MoveAction GetMoveAction()
        {
            return moveAction;
        }

        public override string ToString()
        {
            return gameObject.name;
        }

        
    }
}

