namespace Enemy.Wave
{
    public interface IWave
    {
        void Spawn(float deltaTime);
        int NumberOfTotalWaves();
        int NumberOfPendingWaves();
        int NumberOfSpawnedWaves();
        bool IsCompleted();
    }
}
