using System.Collections.Generic;
using System.Linq;
using Enemy.Wave;
using UnityEngine;

namespace Enemy.Spawn
{
    public static class SpawnParser
    {
        public static IWave Parse(string filePath, EnemySpawner enemySpawner)
        {
            var lines = Resources.Load<TextAsset>(filePath)
                .text.Split('\n');
            
            var index = 0;

            return Read(lines.ToList(), ref index, enemySpawner);
        }

        private static IWave Read(IReadOnlyList<string> waveLines, ref int index, EnemySpawner enemySpawner)
        {
            var subWaves = new List<IWave>();

            while (index < waveLines.Count)
            {
                if (waveLines[index].Trim()[0] == '-')
                {
                    index++;
                    return new ComposeWave(subWaves);
                }

                if (index + 1 < waveLines.Count && int.TryParse(waveLines[index + 1], out _))
                {
                    index++;
                    subWaves.Add(Read(waveLines, ref index, enemySpawner));
                }
                else
                {
                    subWaves.Add(ReadLeafWave(waveLines, ref index, enemySpawner));
                }
            }

            return new ComposeWave(subWaves);
        }

        private static LeafWave ReadLeafWave(
            IReadOnlyList<string> waveLines,
            ref int index,
            EnemySpawner enemySpawner
        )
        {
            var startTime = int.Parse(waveLines[index++]);
            var enemies = new Dictionary<Enemy.Type, int>();

            do
            {
                var enemyInfo = waveLines[index++].Trim().Split(' ');
                enemies.Add(Enemy.GetEnemyType(enemyInfo[0][0]), int.Parse(enemyInfo[1]));
            } while (!int.TryParse(waveLines[index], out _));

            var endTime = int.Parse(waveLines[index++]);
            var wave = new LeafWave(enemies, enemySpawner, startTime, endTime);

            return wave;
        }
    }
}