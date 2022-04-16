namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;

            this.SendEvent<OnEnemyKillEvent>();

            if (UnityEngine.Random.Range(0, 10) < 3)
            {
                gameModel.Gold.Value += UnityEngine.Random.Range(1, 3);
            }

            if (gameModel.KillCount.Value == 4)
            {
                this.SendEvent<GamePassEvent>();
            }
        }
    }
}