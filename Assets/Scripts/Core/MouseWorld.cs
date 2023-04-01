using UnityEngine;

namespace TurnBaseStrategy.Core
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask mousePlaneLayerMask;

        private static MouseWorld instance;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            transform.position = MouseWorld.GetPosition();
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        public static Vector3 GetPosition() 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
            // if the raycast doesnt hit anything then it returns Vector3(0,0,0)
            return raycastHit.point;
        }
    }
}
