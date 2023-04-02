using TMPro;
using UnityEngine;

namespace TurnBaseStrategy.Grid
{ 
    public class GridDebugObject : MonoBehaviour
    {

        private GridObject gridObject;
        [SerializeField] private TextMeshPro debugText;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Update()
        {
            debugText.text = gridObject.ToString();
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        public void SetGridObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
        }
    }
}
