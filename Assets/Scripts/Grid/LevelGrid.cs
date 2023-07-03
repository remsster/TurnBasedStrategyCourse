using System.Collections.Generic;
using UnityEngine;

using TurnBaseStrategy.Core;
using UnityEngine.UI;
using System;

namespace TurnBaseStrategy.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        [SerializeField] private Transform gridDebugObjectPrefab;

        private GridSystem gridSystem;

        public static LevelGrid Instance { get; private set; }

        public event EventHandler OnAnyUnitMovedGridPosition;

        // ----------------------------------------------------------------------------
        // Unity Enging Methods
        // ----------------------------------------------------------------------------

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one LevelGrid " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            gridSystem = new GridSystem(10, 10, 2f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }

        // ----------------------------------------------------------------------------
        // Custom Methods
        // ----------------------------------------------------------------------------

        // -- Public --

        public int GetWidth() => gridSystem.Width;

        public int GetHeight() => gridSystem.Height;

        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
            
        }
        
        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }

        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }

        public void UnitMovedGridPosition(Unit unit, GridPosition from, GridPosition to)
        {
            RemoveUnitAtGridPosition(from,unit);
            AddUnitAtGridPosition(to, unit);
            OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
        }

        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }

        public Unit GetUnitAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnit();
        }

        
    }
}

