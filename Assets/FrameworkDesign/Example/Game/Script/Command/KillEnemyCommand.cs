namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            var gameModel = PointGame.Get<GameModel>();
            gameModel.KillCount.Value++;

            if (gameModel.KillCount.Value == 4)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}