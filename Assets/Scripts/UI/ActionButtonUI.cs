using UnityEngine.UI;
using UnityEngine;
using TMPro;
using TurnBaseStrategy.Action;

namespace TurnBaseStrategy.UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetActionName().ToUpper();
        }
                
    }
}
