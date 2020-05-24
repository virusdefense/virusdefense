using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField] private GameObject pathBlock;
    [SerializeField] private GameObject spawnBlock;
    [SerializeField] private GameObject defenseBlock;
    [SerializeField] private GameObject buildBlock;
    [SerializeField] private GameObject notPlayableBlock;
    [SerializeField] private string levelFilePath;

    private const int BlockSide = 2;

    private void Start()
    {
        var board = ReadBoard(levelFilePath);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;

        for (var i = 0; i < rowNumber; i++)
        {
            for (var j = 0; j < columnNumber; j++)
            {
                var x = Instantiate(
                    GetBlock(board[i][j]),
                    new Vector3(i * BlockSide, 0, j * BlockSide),
                    Quaternion.identity
                );
            }
        }
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