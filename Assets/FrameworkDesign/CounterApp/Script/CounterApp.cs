using FrameworkDesign;

namespace CounterApp
{
    public class CounterApp
    {
        private static IOCContainer _container;

        static void MakeSureContainer()
        {
            if (_container == null)
            {
                _container = new IOCContainer();
                Init();
            }
        }

        private static void Init()
        {
            _container.Register(new CounterModel());
        }

        public static T Get<T>() where T : class
        {
            MakeSureContainer();
            return _container.Get<T>();
        }
        
    }
}