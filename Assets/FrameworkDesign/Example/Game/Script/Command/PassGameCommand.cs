namespace FrameworkDesign.Example
{
    public class PassGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GamePassEvent.Trigger();
        }
    }
}