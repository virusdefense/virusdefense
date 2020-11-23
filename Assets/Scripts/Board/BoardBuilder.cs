using System.Collections.Generic;
using System.Linq;
using Board;
using Enemy.Spawn;
using UnityEngine;
using Utils.Messenger;
using Utils.Path.AStar;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private int level;

    private const int BlockSide = 2;

    private PathFinding _pathFinding;
    private List<List<PathNode>> paths = new List<List<PathNode>>();

    private void Start()
    {
        var board = ReadBoard(level);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;
        var spawnNumber = 0;

        var spawnPoints = new List<Vector2Int>();
        var defensePoints = new List<Vector2Int>();

        var pathGrid = new PathNode[rowNumber, columnNumber];

        for (var i = 0; i < rowNumber; i++)
        {
            for (var j = 0; j < columnNumber; j++)
            {
                var type = Block.GetBlockType(board[i][j]);

                var isWalkable = type == Block.Type.PATH || type == Block.Type.SPAWN || type == Block.Type.DEFENSE;

                if (type == Block.Type.SPAWN)
                    spawnPoints.Add(new Vector2Int(i, j));
                else if (type == Block.Type.DEFENSE)
                    defensePoints.Add(new Vector2Int(i, j));

                pathGrid[i, j] = new PathNode(i, j, isWalkable);

                if (type == Block.Type.SKIP)
                    continue;

                // Build blocks are the double of others block
                var blockShift = type == Block.Type.BUILD ? 1.5f : 1f;

                var block = Instantiate(
                    blocks[(uint) type],
                    new Vector3(i + blockShift, 0, j + blockShift) * BlockSide,
                    Quaternion.identity
                );

                if (type == Block.Type.SPAWN)
                    block.GetComponent<EnemySpawner>().ReadWaves(
                        string.Format(WaveSpawnFile, level.ToString("00"), spawnNumber++)
                    );
            }
        }

        _pathFinding = new PathFinding(pathGrid);

        foreach (var spawnPoint in spawnPoints)
        {
            foreach (var defensePoint in defensePoints)
            {
                paths.Add(_pathFinding.FindPath(spawnPoint, defensePoint));
            }
        }

        /*
        foreach (var path in paths)
        {
            if (path == null)
                continue;

            for (var i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(
                    path[i].RealWorldPosition() + Vector3.one,
                    path[i + 1].RealWorldPosition() + Vector3.one,
                    Color.green,
                    10f
                );
            }
        }
        */

        var realWorldPaths = paths
            .Where(path => path != null)
            .Select(path => path
                .Select(point => point.RealWorldPosition()).ToList()
            ).ToList();

        PathManager.GetInstance().Paths = realWorldPaths;

        Messenger<int>.Broadcast(GameEvent.BOARD_BUILD, spawnNumber);
    }

    private static List<List<char>> ReadBoard(int level)
    {
        return Resources.Load<TextAsset>(string.Format(LevelBoardFile, level.ToString("00")))
            .text.Split('\n')
            .Select(line => line.Split(' ')
                .Select(s => s[0]).ToList()
            ).ToList();
    }

    private const string LevelBoardFile = "Plain/Board/level_{0}";
    private const string WaveSpawnFile = "Plain/Wave/level_{0}_s{1}";
}
