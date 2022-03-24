namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            GameModel.Instance.KillCount.Value++;

            if (GameModel.Instance.KillCount.Value == 4)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}