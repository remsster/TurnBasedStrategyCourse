using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class Unit : MonoBehaviour
    {

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float stoppingDistance = .1f;

        private Vector3 targetPosition;


        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Update()
        {
            if (Vector3.Distance(transform.position,targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Move(MouseWorld.GetPosition());
            }
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        private void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}

