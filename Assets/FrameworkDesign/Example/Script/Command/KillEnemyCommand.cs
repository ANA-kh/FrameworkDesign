namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            GameModel.KillCount.Value++;

            if (GameModel.KillCount.Value == 4)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}