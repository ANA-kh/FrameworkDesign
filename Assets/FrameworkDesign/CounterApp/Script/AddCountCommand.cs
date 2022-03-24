
using FrameworkDesign;

namespace CounterApp
{
    public struct AddCountCommand : ICommand
    {
        public void Execute()
        {
            CounterModel.Instance.Count.Value++;
        }
    }
}