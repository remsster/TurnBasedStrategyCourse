using System.Collections.Generic;

using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.Grid
{
    public class GridObject
    {
        private GridSystem gridSystem;
        private GridPosition gridPosition;
        private List<Unit> unitList;

        //public Unit Unit { get => unit; set { unit = value; } }

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;
            unitList = new List<Unit>();
        }

        public override string ToString()
        {
            string unitString = "";
            foreach(Unit unit in unitList)
            {
                unitString += unit + "\n";
            }
            return unitString + " " + gridPosition.ToString();
        }

        public void AddUnit(Unit unit)
        {
            unitList.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            unitList.Remove(unit);
        }

        public List<Unit> GetUnitList()
        {
            return unitList;
        }

        public bool HasAnyUnit() => unitList.Count > 0;

        public Unit GetUnit() => (HasAnyUnit()) ? unitList[0] : null;
        
            
    }
}
