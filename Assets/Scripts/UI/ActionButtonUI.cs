using UnityEngine.UI;
using UnityEngine;
using TMPro;
using TurnBaseStrategy.Action;
using TurnBaseStrategy.Core;

namespace TurnBaseStrategy.UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        [SerializeField] private Image selectedVisualImage;

        private BaseAction baseAction;

        public void SetBaseAction(BaseAction baseAction)
        {
            this.baseAction = baseAction;
            textMeshPro.text = baseAction.GetActionName().ToUpper();
            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
               
            });
        }

        public void UpdateSelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
            selectedVisualImage.enabled = selectedBaseAction == baseAction;
        }
    }
}
