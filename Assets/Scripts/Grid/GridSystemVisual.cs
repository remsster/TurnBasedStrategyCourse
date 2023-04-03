using System.Collections.Generic;
using TurnBaseStrategy.Core;
using UnityEngine;

namespace TurnBaseStrategy.Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] gridSystemVisualSingleArrray;

        public static GridSystemVisual Instance { get; private set; }

        // ----------------------------------------------------------------------------
        // Unity Engine Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("There is more than one GridSystemVisualInstance " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }



        private void Start()
        {
            gridSystemVisualSingleArrray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetWidth(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridSystemVisualSingleInstance = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                    gridSystemVisualSingleArrray[x, z] = gridSystemVisualSingleInstance.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        public void HideAllGridPositions()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetWidth(); z++)
                {
                    gridSystemVisualSingleArrray[x, z].Hide();
                }
            }
        }

        public void ShowAllGridPositions(List<GridPosition> gridPositionList)
        {
            foreach(GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArrray[gridPosition.X, gridPosition.Z].Show();
            }
        }

        private void UpdateGridVisual()
        {
            GridSystemVisual.Instance.HideAllGridPositions();
            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            GridSystemVisual.Instance.ShowAllGridPositions(selectedUnit.GetMoveAction().GetValidActionGridPostionList());
        }


    }
}
