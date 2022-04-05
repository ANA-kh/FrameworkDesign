using FrameworkDesign;

namespace CounterApp
{
    public class SubCountCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GetArchitecture().GetModel<ICounterModel>().Count.Value--;
        }
    }
}