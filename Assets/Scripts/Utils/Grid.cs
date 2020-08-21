using UnityEngine;

namespace Utils
{
    public class Grid<T>
    {
        private int cellSide;
        private Vector3 origin;
        private int width;
        private int height;
        private T[,] gridArray;

        public Grid(T[,] gridArray, int cellSide, Vector3 origin = default, bool isDebug = false)
        {
            this.cellSide = cellSide;
            this.origin = origin;
            this.gridArray = gridArray;
            width = gridArray.GetLength(0);
            height = gridArray.GetLength(1);

            if (isDebug)
                PrintGrid();
        }

        public Grid(int width, int height, int cellSide, Vector3 origin = default, bool isDebug = false)
        {
            this.width = width;
            this.height = height;
            this.cellSide = cellSide;
            this.origin = origin;

            gridArray = new T[width, height];

            if (isDebug)
                PrintGrid();
        }

        public T this[int i, int j]
        {
            get => gridArray[i, j];
            set => gridArray[i, j] = value;
        }

        public T this[Vector3 position]
        {
            get => GetValue(position);
            set => SetValue(position, value);
        }

        private T GetValue(Vector3 position)
        {
            var cell = GetGridCords(position);
            return gridArray[cell.x, cell.y];
        }

        private void SetValue(Vector3 position, T value)
        {
            var cell = GetGridCords(position);
            gridArray[cell.x, cell.y] = value;
        }

        public Vector3 GetRealWorldPosition(int x, int z)
        {
            return new Vector3(x + 0.5f, 0, z + 0.5f) * cellSide + origin;
        }

        public Vector2Int GetGridCords(Vector3 position)
        {
            var relativePosition = position - origin;
            // Mathf.FloorToInt
            var x = Mathf.RoundToInt(relativePosition.x / cellSide);
            var z = Mathf.RoundToInt(relativePosition.z / cellSide);
            return new Vector2Int(x, z);
        }

        private void PrintGrid()
        {
            for (var x = 0; x < width; x++)
            {
                for (var z = 0; z < height; z++)
                {
                    Debug.DrawLine(GetRealWorldPosition(x, z), GetRealWorldPosition(x, z + 1), Color.red, 100f);
                    Debug.DrawLine(GetRealWorldPosition(x, z), GetRealWorldPosition(x + 1, z), Color.red, 100f);
                }
            }

            Debug.DrawLine(GetRealWorldPosition(0, height), GetRealWorldPosition(width, height), Color.red, 100f);
            Debug.DrawLine(GetRealWorldPosition(width, 0), GetRealWorldPosition(width, height), Color.red, 100f);
            //GUI.Label(new Rect(0, 0, cellSide, cellSide), "x");
        }
    }
}