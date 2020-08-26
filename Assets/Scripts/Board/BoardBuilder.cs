using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Board;
using UnityEngine;
using Utils;
using Utils.Path.AStar;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField] private GameObject pathBlock;
    [SerializeField] private GameObject spawnBlock;
    [SerializeField] private GameObject defenseBlock;
    [SerializeField] private GameObject buildBlock;
    [SerializeField] private GameObject notPlayableBlock;
    [SerializeField] private string levelFilePath;

    private const int BlockSide = 2;

    private PathFinding _pathFinding;
    private List<List<PathNode>> paths = new List<List<PathNode>>();

    private void Start()
    {
        var board = ReadBoard(levelFilePath);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;

        var spawnPoints = new List<Vector2Int>();
        var defensePoints = new List<Vector2Int>();

        var pathGrid = new PathNode[rowNumber, columnNumber];

        for (var i = 0; i < rowNumber; i++)
        {
            for (var j = 0; j < columnNumber; j++)
            {
                var type = board[i][j];

                var x = Instantiate(
                    GetBlock(type),
                    new Vector3((i + 1) * BlockSide, 0, (j + 1) * BlockSide),
                    Quaternion.identity
                );

                var isWalkable = type == 'P' || type == 'S' || type == 'D';

                if (type == 'S')
                    spawnPoints.Add(new Vector2Int(i, j));
                else if (type == 'D')
                    defensePoints.Add(new Vector2Int(i, j));

                pathGrid[i, j] = new PathNode(i, j, isWalkable);
            }
        }

        _pathFinding = new PathFinding(pathGrid, isDebug: true);

        foreach (var spawnPoint in spawnPoints)
        {
            foreach (var defensePoint in defensePoints)
            {
                paths.Add(_pathFinding.FindPath(spawnPoint, defensePoint));
            }
        }

        foreach (var path in paths)
        {
            if (path == null)
                continue;

            Debug.Log("Path:");
            for (var i = 0; i < path.Count - 1; i++)
            {
                Debug.Log($"\tx: {path[i].X}, Y: {path[i].Y}");
                Debug.DrawLine(
                    path[i].RealWorldPosition() + Vector3.one,
                    path[i + 1].RealWorldPosition() + Vector3.one,
                    Color.green,
                    10f
                );
            }

            Debug.Log("============");
        }

        var realWorldPaths = paths
            .Select(path => path
                .Select(point => point.RealWorldPosition()).ToList()
            ).ToList();

        PathManager.GetInstance().Paths = realWorldPaths;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Mouse.GetMouseWorldPosition(out var position)) return;


        var path = _pathFinding.FindPath(new Vector3(2, 0, 8), position);

        Debug.Log("Path:");
        for (var i = 0; i < path.Count - 1; i++)
        {
            Debug.Log($"\tx: {path[i].X}, Y: {path[i].Y}");
            Debug.DrawLine(
                path[i].RealWorldPosition() + Vector3.one,
                path[i + 1].RealWorldPosition() + Vector3.one,
                Color.green,
                2f
            );
        }

        Debug.Log("============");
    }

    private static List<List<char>> ReadBoard(string filePath)
    {
        var reader = new StreamReader(filePath);
        var lines = reader.ReadToEnd().Split(
            new[] {"\r\n", "\r", "\n"},
            StringSplitOptions.None
        );
        reader.Close();

        return lines
            .Select(line => line.Split(' ')
                .Select(s => s[0]).ToList())
            .ToList();
    }

    private GameObject GetBlock(char type)
    {
        switch (type)
        {
            case 'T': return buildBlock;
            case 'D': return defenseBlock;
            case 'S': return spawnBlock;
            case 'P': return pathBlock;
        }

        return notPlayableBlock;
    }
}