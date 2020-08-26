using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Path.AStar
{
    public class PathFinding
    {
        private const int StraightCost = 10;
        private const int DiagonalCost = 14;

        private readonly int _width;
        private readonly int _height;

        private readonly Grid<PathNode> _grid;

        private List<PathNode> _openList;
        private List<PathNode> _closeList;

        public PathFinding(int width, int height)
        {
            _width = width;
            _height = height;

            _grid = new Grid<PathNode>(width, height, 2, isDebug: true);

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                    _grid[i, j] = new PathNode(i, j, true, _grid);
            }
        }

        public PathFinding(PathNode[,] grid, bool isDebug = false)
        {
            _grid = new Grid<PathNode>(grid, 2, isDebug: isDebug);
            _width = _grid.Width;
            _height = _grid.Height;

            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    _grid[i, j].Grid = _grid;
                }
            }
        }

        public List<PathNode> FindPath(Vector3 startPosition, Vector3 endPosition)
        {
            var start = _grid.GetGridCords(startPosition);
            var end = _grid.GetGridCords(endPosition);
            Debug.Log($"start: {start}");
            Debug.Log($"end: {end}");
            return FindPath(start.x, start.y, end.x, end.y);
        }

        public List<PathNode> FindPath(Vector2Int start, Vector2Int end)
        {
            return FindPath(start.x, start.y, end.x, end.y);
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            Reset();

            var startNode = _grid[startX, startY];
            var endNode = _grid[endX, endY];

            _openList = new List<PathNode> {startNode};
            _closeList = new List<PathNode>();

            startNode.GCost = 0;
            startNode.HCost = FreeSpaceDistance(startNode, endNode);

            while (_openList.Count > 0)
            {
                var currentNode = GetLowerNode(_openList);

                if (currentNode == endNode)
                    return BuildPath(endNode);

                _openList.Remove(currentNode);
                _closeList.Add(currentNode);

                foreach (
                    var neighbour in _grid.GetNeighbour(currentNode.X, currentNode.Y)
                        .Where(neighbour => !_closeList.Contains(neighbour))
                )
                {
                    if (!neighbour.IsWalkable)
                    {
                        _closeList.Add(neighbour);
                        continue;
                    }

                    var tentativeGCost = currentNode.GCost + FreeSpaceDistance(currentNode, neighbour);
                    if (tentativeGCost >= neighbour.GCost) continue;
                    neighbour.PreviousNode = currentNode;
                    neighbour.GCost = tentativeGCost;
                    neighbour.HCost = FreeSpaceDistance(neighbour, endNode);

                    if (!_openList.Contains(neighbour))
                        _openList.Add(neighbour);
                }
            }

            return null;
        }

        private void Reset()
        {
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                    _grid[i, j].Reset();
            }
        }

        private static List<PathNode> BuildPath(PathNode endNode)
        {
            var path = new List<PathNode> {endNode};
            var currentNode = endNode.PreviousNode;

            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.PreviousNode;
            }

            path.Reverse();
            return path;
        }

        private static int FreeSpaceDistance(PathNode a, PathNode b)
        {
            var distanceX = Math.Abs(a.X - b.X);
            var distanceY = Math.Abs(a.Y - b.Y);
            var remaining = Math.Abs(distanceX - distanceY);

            return Math.Min(distanceX, distanceY) * DiagonalCost + remaining * StraightCost;
        }

        private static PathNode GetLowerNode(IEnumerable<PathNode> nodeList)
        {
            return nodeList.Min();
        }
    }
}