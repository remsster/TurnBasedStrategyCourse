using System.Collections.Generic;

using UnityEngine;

using TurnBaseStrategy.Core;
using TurnBaseStrategy.Grid;

namespace TurnBaseStrategy.Action
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;

        protected bool isActive;

        protected System.Action<bool> onActionComplete;

        public static event System.EventHandler OnAnyActionStarted;
        public static event System.EventHandler OnAnyActionCompleted;

        // -- Protected --
        protected virtual void Awake()
        {
            unit = GetComponent<Unit>(); 
        }

        protected void ActionStart(System.Action<bool> onActionComplete)
        {
            isActive = true;
            this.onActionComplete = onActionComplete;
            OnAnyActionStarted?.Invoke(this, System.EventArgs.Empty);
        }

        protected void ActionComplete()
        {
            isActive = false;
            // on action complete sets isBusy to false
            onActionComplete(false);
            OnAnyActionCompleted?.Invoke(this, System.EventArgs.Empty);
        }

        // -- Public --

        public abstract string GetActionName();

        public abstract void TakeAction(GridPosition gridPosition, System.Action<bool> onActionComplete);

        public abstract List<GridPosition> GetValidActionGridPostionList();

        public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPostionList();
            return validGridPositionList.Contains(gridPosition);
        }

        /// <summary>
        ///  By default, all actions have a cost of 1
        /// </summary>
        /// <returns></returns>
        public virtual int GetActionPointsCost()
        {
            return 1;
        }

        public Unit GetUnit() => unit;

    }
}
