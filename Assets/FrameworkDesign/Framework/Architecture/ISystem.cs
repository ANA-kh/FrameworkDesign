namespace FrameworkDesign
{
    public interface ISystem : IBelongToArchitecture, ICanSetArchitecture, 
        ICanGetModel, ICanGetUtility, ICanSendEvent,ICanRegisterEvent
    {
        void Init();
    }

    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture _architecture;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return _architecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        void ISystem.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }
}