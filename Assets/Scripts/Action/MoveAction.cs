using System;
using System.Collections.Generic;
using UnityEngine;

using TurnBaseStrategy.Grid;
using UnityEngine.EventSystems;

namespace TurnBaseStrategy.Action
{
    public class MoveAction : BaseAction
    {

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float stoppingDistance = .1f;
        [SerializeField] private int maxMoveDistance = 4;

        private Vector3 targetPosition;

        public event EventHandler OnStartMoving;
        public event EventHandler OnStopMoving;


        // ----------------------------------------------------------------------------
        // Unity Engine Methods
        // ----------------------------------------------------------------------------

        protected override void Awake()
        {
            base.Awake();
            targetPosition = transform.position;
        }

        private void Update()
        {
            if (!isActive) return;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
            else
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        public override string GetActionName() => "Move";
        

        public override void TakeAction(GridPosition gridPosition, System.Action<bool> onActionComplete)
        {
            ActionStart(onActionComplete);
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            OnStartMoving?.Invoke(this, EventArgs.Empty);
        }

        public override List<GridPosition> GetValidActionGridPostionList()
        {

            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GridPosition;

            for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    // Checks

                    // check if the position is on the grid
                    if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    // check if the unit position is the current grid position
                    if (unit.GridPosition == testGridPosition) continue;
                    // check if there is another unit on the grid movement area
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }
    }
}
