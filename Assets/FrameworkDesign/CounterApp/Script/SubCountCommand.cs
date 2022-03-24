using FrameworkDesign;

namespace CounterApp
{
    public struct SubCountCommand : ICommand
    {
        public void Execute()
        {
            CounterModel.Instance.Count.Value--;
        }
    }
}