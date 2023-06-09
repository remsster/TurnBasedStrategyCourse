using System;
using UnityEngine;
using TurnBaseStrategy.Grid;
using System.Collections.Generic;

namespace TurnBaseStrategy.Action
{
    public class SpinAction : BaseAction
    {
        private float totalSpinAmount;

        // private SpinCompleteDelegate onSpinComplete;

        //public delegate void SpinCompleteDelegate(bool value);

        private void Update()
        {
            if (!isActive) return;
        
            float spinAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAmount,0);

            totalSpinAmount += spinAmount;
            if (totalSpinAmount >= 360f)
            {
                ActionComplete();
            }
        }

        public override string GetActionName() => "Spin";

        //public void Spin(SpinCompleteDelegate onSpinComplete)
        public override void TakeAction(GridPosition gridPosition, System.Action onActionComplete)
        {
            totalSpinAmount = 0;
            ActionStart(onActionComplete);
        }

        public override List<GridPosition> GetValidActionGridPostionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GridPosition;
            return new List<GridPosition> { unitGridPosition };
        }

        public override int GetActionPointsCost()
        {
            return 2;
        }
    }
}
