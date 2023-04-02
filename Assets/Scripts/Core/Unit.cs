using TurnBaseStrategy.Grid;
using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float stoppingDistance = .1f;

        [SerializeField] private Animator unitAnimator;

        private GridPosition gridPosition;

        private Vector3 targetPosition;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        private void Update()
        {

            if (Vector3.Distance(transform.position,targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward,moveDirection,rotateSpeed * Time.deltaTime);
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }

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

        public override string ToString()
        {
            return gameObject.name;
        }

        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}

