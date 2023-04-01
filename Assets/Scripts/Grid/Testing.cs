using UnityEngine;

using TurnBaseStrategy.Grid;
using TurnBaseStrategy.Core;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;
    // Start is called before the first frame update
    private void Start()
    {
        gridSystem = new GridSystem(10, 10,2f);
        Debug.Log(new GridPosition(5, 7));
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
