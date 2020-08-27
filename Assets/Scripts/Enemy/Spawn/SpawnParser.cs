using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemy.Wave;
using UnityEngine;

namespace Enemy.Spawn
{
    public static class SpawnParser
    {
        public static IWave Parse(string filePath, EnemySpawner enemySpawner)
        {
            var reader = new StreamReader(filePath);
            var lines = reader.ReadToEnd().Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None
            );

            return ReadWaves(lines.ToList(), enemySpawner);
        }

        private static IWave ReadWaves(IReadOnlyList<string> waveLines, EnemySpawner enemySpawner)
        {
            var index = 0;
            var waves = new List<IWave>();

            while (index < waveLines.Count)
            {
                Debug.Log(waveLines[index]);
                var startTime = int.Parse(waveLines[index++]);


                if (!int.TryParse(waveLines[index], out _))
                    waves.Add(ReadLeafWave(waveLines, enemySpawner, startTime, ref index));
            }

            return new Wave.Wave(waves);
        }

        private static LeafWave ReadLeafWave(
            IReadOnlyList<string> waveLines,
            EnemySpawner enemySpawner,
            int startTime,
            ref int index
        )
        {
            var enemies = new Dictionary<EnemyType, int>();

            do
            {
                var enemyInfo = waveLines[index++].Trim().Split(' ');
                enemies.Add(GetType(enemyInfo[0]), int.Parse(enemyInfo[1]));
            } while (!int.TryParse(waveLines[index], out _));

            var endTime = int.Parse(waveLines[index++]);
            var wave = new LeafWave(enemies, enemySpawner, startTime, endTime);

            return wave;
        }

        private static EnemyType GetType(string enemyType)
        {
            switch (enemyType)
            {
                case "A": return EnemyType.A;
                case "B": return EnemyType.B;
            }

            return EnemyType.A;
        }
    }
}