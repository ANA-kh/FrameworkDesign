namespace FrameworkDesign
{
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture
    {
        void Init();
    }
    
    public abstract class AbstractModel :IModel
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

        void IModel.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }
}