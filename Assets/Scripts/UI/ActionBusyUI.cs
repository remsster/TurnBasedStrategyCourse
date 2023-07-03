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
            Hide();
        }

        private void UnitActionSystem_IsBusyChanged(object sender, bool isBusy)
        {
            if (isBusy) Show();
            else Hide();
        }

        private void Show()
        {
            actionBusy.SetActive(true);
        }

        private void Hide()
        {
            actionBusy.SetActive(false);
        }

    }
}
