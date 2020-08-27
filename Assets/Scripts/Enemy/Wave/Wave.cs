using System.Collections.Generic;

namespace Enemy.Wave
{
    public class Wave: IWave
    {
        private readonly List<IWave> _subWaves;

        public Wave(List<IWave> subWaves)
        {
            _subWaves = subWaves;
        } 

        public void Spawn(float deltaTime)
        {
            _subWaves.ForEach(wave => wave.Spawn(deltaTime));
        }
    }
}