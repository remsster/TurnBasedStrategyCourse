using TMPro;
using UnityEngine;

namespace TurnBaseStrategy.Grid
{ 
    public class GridDebugObject : MonoBehaviour
    {

        private GridObject gridObject;
        [SerializeField] private TextMeshPro debugText;

        private void Update()
        {
            debugText.text = gridObject.ToString();
        }

        public void SetGridObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
        }
    }
}
