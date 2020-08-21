using UnityEngine;
using Utils;
using Grid = Utils.Grid<int>;

namespace Board
{
    public class BoardLogic : MonoBehaviour
    {
        private Grid _grid;

        private void Start()
        {
            const int width = 13;
            const int height = 20;

            _grid = new Grid(width, height, 2, isDebug: true);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    _grid[i, j] = 0;
                }
            }
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Mouse.GetMouseWorldPosition(out var position)) return;

            _grid[position]++;
            var cell = _grid.GetGridCords(position);
            Debug.Log($"x: {cell.x}; y: {cell.y} = {_grid[position]}");
        }
    }
}