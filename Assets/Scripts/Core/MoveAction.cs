using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TurnBaseStrategy.Grid;
using UnityEngine;
using UnityEngine.UIElements;

namespace TurnBaseStrategy.Core
{
    public class MoveAction : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float stoppingDistance = .1f;
        [SerializeField] private int maxMoveDistance = 4;


        [SerializeField] private Animator unitAnimator;

        private Vector3 targetPosition;
        private Unit unit;


        // ----------------------------------------------------------------------------
        // Unity Engine Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            unit = GetComponent<Unit>();
        }

        private void Start()
        {
            targetPosition = transform.position;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        public void Move(GridPosition gridPosition)
        {
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        }

        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPostionList();
            return validGridPositionList.Contains(gridPosition);
        }

        public List<GridPosition> GetValidActionGridPostionList()
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
