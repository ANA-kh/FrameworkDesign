namespace FrameworkDesign
{
    public interface ICommand :IBelongToArchitecture,ICanSetArchitecture
    {
        void Execute();
    }
    
    public abstract class AbstractCommand :ICommand
    {
        private IArchitecture _architecture;
        public IArchitecture GetArchitecture()
        {
            return _architecture;
        }

        public void SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        void ICommand.Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();
    }
}