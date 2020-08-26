using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Board
{
    public class PathManager
    {
        protected static PathManager Instance;
        public List<List<Vector3>> Paths;

        public static PathManager GetInstance()
        {
            if (Instance == null)
                Instance = new PathManager();

            return Instance;
        }

        public List<Vector3> SelectNearestPath(Vector3 startPoint)
        {
            return Paths.Aggregate(
                (nearest, next) =>
                    Vector3.Distance(next[0], startPoint) < Vector3.Distance(nearest[0], startPoint) 
                        ? next : nearest
            );
        }
    }
}