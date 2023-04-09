using TurnBaseStrategy.Core;
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
    }
}
