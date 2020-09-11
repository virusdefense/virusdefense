using System;
using UnityEngine;

namespace Utils.Path.AStar
{
    public class PathNode : IComparable
    {
        public Grid<PathNode> Grid;
        public readonly int X;
        public readonly int Y;
        public readonly bool IsWalkable;

        public int GCost = int.MaxValue;
        public int HCost;

        public int FCost => GCost + HCost;

        public PathNode PreviousNode;

        public PathNode(int x, int y, bool isWalkable, Grid<PathNode> grid)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
            Grid = grid;
        }

        public PathNode(int x, int y, bool isWalkable)
        {
            X = x;
            Y = y;
            IsWalkable = isWalkable;
        }

        public Vector3 RealWorldPosition()
        {
            return Grid.GetRealWorldPosition(X, Y);
        }

        public void Reset()
        {
            GCost = int.MaxValue;
            HCost = 0;
            PreviousNode = null;
        }

        public int CompareTo(object obj)
        {
            return FCost.CompareTo(((PathNode) obj).FCost);
        }
    }
}