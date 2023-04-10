using System;
using System.Collections.Generic;
using TurnBaseStrategy.Core;
using TurnBaseStrategy.Grid;
using UnityEngine;

namespace TurnBaseStrategy.Action
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;

        protected bool isActive;

        protected System.Action<bool> onActionComplete;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>(); 
        }

        public abstract string GetActionName();

        public abstract void TakeAction(GridPosition gridPosition, System.Action<bool> onActionComplete);

        public abstract List<GridPosition> GetValidActionGridPostionList();

        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPostionList();
            return validGridPositionList.Contains(gridPosition);
        }

    }
}
