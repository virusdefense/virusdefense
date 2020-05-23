using System;
using System.Collections;
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
    [SerializeField] private String levelFilePath;

    private int blockSide = 2;

    private void Start()
    {
        var board = ReadBoard(levelFilePath);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;

        for (var i = 0; i < rowNumber; i++)
        {
            for (var j = 0; j < columnNumber; j++)
            {
                Instantiate(getBlock(board[i][j]))
                    .transform.position = new Vector3(i * blockSide, 0, j * blockSide);
            }
        }
    }

    private static List<List<char>> ReadBoard(String filePath)
    {
        var reader = new StreamReader(filePath);
        var lines = reader.ReadToEnd().Split(
            new[] {"\r\n", "\r", "\n"},
            StringSplitOptions.None
        );
        reader.Close();

        return lines
            .Select(line => line.Split(new[] {' '})
                .Select(s => s[0]).ToList())
            .ToList();
    }

    private GameObject getBlock(char type)
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