using UnityEngine;

namespace TurnBaseStrategy.Grid
{
    public class GridSystem
    {
        private int width;
        private int height;
        private float cellSize;

        private GridObject[,] gridObjectArray;

        public GridSystem(int width, int height,float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;


            gridObjectArray = new GridObject[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    gridObjectArray[x,z] = new GridObject(this, new GridPosition(x,z));
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / cellSize),
                Mathf.RoundToInt(worldPosition.z / cellSize));
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
                }
            }
        }
    }
}
