using System.Collections.Generic;
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

        

        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
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
                    Debug.Log(testGridPosition);
                }
            }

            return validGridPositionList;
        }
    }
}
