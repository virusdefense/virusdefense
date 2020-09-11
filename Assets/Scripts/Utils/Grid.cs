using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Grid<T>
    {
        private readonly int _cellSide;
        private readonly Vector3 _origin;
        private readonly T[,] _gridArray;

        public int Width { get; }

        public int Height { get; }

        public Grid(T[,] gridArray, int cellSide, Vector3 origin = default, bool isDebug = false)
        {
            _cellSide = cellSide;
            _origin = origin;
            _gridArray = gridArray;
            Width = gridArray.GetLength(0);
            Height = gridArray.GetLength(1);

            if (isDebug)
                PrintGrid();
        }

        public Grid(int width, int height, int cellSide, Vector3 origin = default, bool isDebug = false)
        {
            Width = width;
            Height = height;
            _cellSide = cellSide;
            _origin = origin;

            _gridArray = new T[width, height];

            if (isDebug)
                PrintGrid();
        }

        public T this[int i, int j]
        {
            get => _gridArray[i, j];
            set => _gridArray[i, j] = value;
        }

        public T this[Vector3 position]
        {
            get => GetValue(position);
            set => SetValue(position, value);
        }

        private T GetValue(Vector3 position)
        {
            var cell = GetGridCords(position);
            return _gridArray[cell.x, cell.y];
        }

        private void SetValue(Vector3 position, T value)
        {
            var cell = GetGridCords(position);
            _gridArray[cell.x, cell.y] = value;
        }

        public Vector3 GetRealWorldPosition(int x, int z)
        {
            return new Vector3(x + 0.5f, 0, z + 0.5f) * _cellSide + _origin;
        }

        public Vector2Int GetGridCords(Vector3 position)
        {
            var relativePosition = position - _origin;
            // Mathf.FloorToInt
            var x = Mathf.RoundToInt(relativePosition.x / _cellSide) - 1;
            var z = Mathf.RoundToInt(relativePosition.z / _cellSide) - 1;
            return new Vector2Int(x, z);
        }

        public List<T> GetNeighbour(int x, int y)
        {
            var neighbourList = new List<T>();
            if (x - 1 >= 0)
            {
                // Left
                neighbourList.Add(_gridArray[x - 1, y]);
                // Left Down
                if (y + 1 < Height)
                    neighbourList.Add(_gridArray[x - 1, y + 1]);
                // Left Up
                if (y - 1 >= 0)
                    neighbourList.Add(_gridArray[x - 1, y - 1]);
            }

            if (x + 1 < Width)
            {
                // Right
                neighbourList.Add(_gridArray[x + 1, y]);
                // Right Down
                if (y + 1 < Height)
                    neighbourList.Add(_gridArray[x + 1, y + 1]);
                // Right Up
                if (y - 1 >= 0)
                    neighbourList.Add(_gridArray[x + 1, y - 1]);
            }

            // Down
            if (y + 1 < Height)
                neighbourList.Add(_gridArray[x, y + 1]);
            // Up
            if (y - 1 >= 0)
                neighbourList.Add(_gridArray[x, y - 1]);

            return neighbourList;
        }

        private void PrintGrid()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var z = 0; z < Height; z++)
                {
                    Debug.DrawLine(GetRealWorldPosition(x, z), GetRealWorldPosition(x, z + 1), Color.red, 100f);
                    Debug.DrawLine(GetRealWorldPosition(x, z), GetRealWorldPosition(x + 1, z), Color.red, 100f);
                }
            }

            Debug.DrawLine(GetRealWorldPosition(0, Height), GetRealWorldPosition(Width, Height), Color.red, 100f);
            Debug.DrawLine(GetRealWorldPosition(Width, 0), GetRealWorldPosition(Width, Height), Color.red, 100f);
            //GUI.Label(new Rect(0, 0, cellSide, cellSide), "x");
        }
    }
}