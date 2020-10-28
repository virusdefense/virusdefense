using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy.Wave
{
    public class ComposeWave : IWave
    {
        private readonly List<IWave> _subWaves;

        public ComposeWave(List<IWave> subWaves)
        {
            _subWaves = subWaves;
        }

        public bool IsCompleted()
        {
            return _subWaves
                .Aggregate(true, (acc, wave) => acc && wave.IsCompleted());
        }

        public bool IsStarted()
        {
            return _subWaves
                .Aggregate(false, (acc, wave) => acc || wave.IsStarted());
        }

        public int NumberOfTotalWaves()
        {
            return _subWaves.Count;
        }

        public int NumberOfSpawnedWaves()
        {
            return _subWaves
                .Aggregate(0, (acc, wave) => wave.IsCompleted() ? acc + 1 : acc);
        }

        public int NumberOfStartedWaves()
        {
            return _subWaves
                .Aggregate(0, (acc, wave) => wave.IsStarted() ? acc + 1 : acc);
        }

        public int NumberOfPendingWaves()
        {
            Debug.Log($"Tot: {NumberOfTotalWaves()}, Spaw: {NumberOfSpawnedWaves()}");
            return NumberOfTotalWaves() - NumberOfSpawnedWaves();
        }

        public void Spawn(float deltaTime)
        {
            _subWaves.ForEach(wave => wave.Spawn(deltaTime));
        }

        public override string ToString()
        {
            var str = "Compose wave start";
            _subWaves.ForEach(wave => str += $"\n{wave}");
            return str + "\nCompose wave end";
        }
    }
}
