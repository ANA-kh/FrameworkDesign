namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            GameModel.KillCount.Value++;
        }
    }
}