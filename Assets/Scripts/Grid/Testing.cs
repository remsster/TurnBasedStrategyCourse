using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Core;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            unit.GetMoveAction().GetValidActionGridPostionList();
        }
    }
}
