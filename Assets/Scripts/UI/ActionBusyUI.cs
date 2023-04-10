using UnityEngine;

using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.UI
{
    public class ActionBusyUI : MonoBehaviour
    {
        [SerializeField] private GameObject actionBusy;

        private void Start()
        {
            UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_IsBusyChanged;
            actionBusy.SetActive(false);
        }

        private void UnitActionSystem_IsBusyChanged(object sender, bool value)
        {
            actionBusy.SetActive(value);
        }

    }
}
