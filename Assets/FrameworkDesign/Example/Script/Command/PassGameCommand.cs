namespace FrameworkDesign.Example
{
    public class PassGameCommand : ICommand
    {
        public void Execute()
        {
            GamePassEvent.Trigger();
        }
    }
}