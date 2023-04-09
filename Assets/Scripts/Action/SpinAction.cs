using System;
using UnityEngine;


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
                isActive = false;
                base.onActionComplete(false);
            }
        }

        //public void Spin(SpinCompleteDelegate onSpinComplete)
        public void Spin(Action<bool> onActionComplete)
        {
            isActive = true;
            totalSpinAmount = 0;
            this.onActionComplete = onActionComplete;
            
        }
    }
}
