namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;

            this.SendEvent<OnEnemyKillEvent>();

            if (gameModel.KillCount.Value == 4)
            {
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}