using System.Collections.Generic;

namespace Enemy.Wave
{
    public class ComposeWave : IWave
    {
        private readonly List<IWave> _subWaves;

        public ComposeWave(List<IWave> subWaves)
        {
            _subWaves = subWaves;
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

        public int Count()
        {
            return _subWaves.Count;
        }
    }
}