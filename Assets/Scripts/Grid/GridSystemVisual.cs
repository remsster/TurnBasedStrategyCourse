using System;
using System.Collections.Generic;
using TurnBaseStrategy.Action;
using TurnBaseStrategy.Core;
using UnityEngine;
using UnityEngine.Rendering;

namespace TurnBaseStrategy.Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;

        private GridSystemVisualSingle[,] gridSystemVisualSingleArrray;

        public static GridSystemVisual Instance { get; private set; }

        [Serializable]
        public struct GridVisualTypeMaterial
        {
            public GridVisualType gridVisualType;
            public Material material;

        }

        public enum GridVisualType
        {
            White,
            Blue,
            Red,
            Yellow,
            RedSoft
        }

        

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

            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            LevelGrid.Instance.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;

            UpdateGridVisual();
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        // -- Private --

        private void ShowGridPositionRange(GridPosition gridPosition,int range, GridVisualType gridVisualType)
        {
            List<GridPosition> gridPositionsList = new List<GridPosition>();
            for (int x = -range; x <= range; x++)
            {
                for (int z = -range; z <= range; z++)
                {
                    GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

                    int testDistance = Math.Abs(x) + Math.Abs(z);
                    if (testDistance > range) continue;

                    gridPositionsList.Add(testGridPosition);    
                }

            }

            ShowGridPositionList(gridPositionsList, gridVisualType);
        }

        private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
        {
            foreach(GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
            {
                if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
                {
                    return gridVisualTypeMaterial.material;
                }
            }
            Debug.LogError("Coult not find GridVisualTypeMaterial for GridVisualType " + gridVisualType);
            return null;
        }

        private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, EventArgs e)
        {
            UpdateGridVisual();
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
        {
            UpdateGridVisual();
        }

        private void UpdateGridVisual()
        {
            GridSystemVisual.Instance.HideAllGridPositions();
            Unit selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

            GridVisualType gridVisualType;
            switch (selectedAction)
            {
                default:
                case MoveAction moveAction:
                    gridVisualType = GridVisualType.White;
                    break;
                case SpinAction spinAction :
                    gridVisualType = GridVisualType.Blue;
                    break;
                case ShootAction shootAction:
                    gridVisualType = GridVisualType.Red;
                    ShowGridPositionRange(selectedUnit.GridPosition,shootAction.GetMaxShootDistance(),GridVisualType.RedSoft);
                    break;
            }
            GridSystemVisual.Instance.ShowGridPositionList(selectedAction.GetValidActionGridPostionList(),gridVisualType);
        }

        // -- Public --

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

        public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
        {
            foreach(GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArrray[gridPosition.X, gridPosition.Z].Show(GetGridVisualTypeMaterial(gridVisualType));
            }
        }

        


    }
}
