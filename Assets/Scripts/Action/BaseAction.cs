using TurnBaseStrategy.Core;
using UnityEngine;

namespace TurnBaseStrategy.Action
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;

        protected bool isActive;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>(); 
        }
    }
}