namespace FrameworkDesign
{
    public abstract class Architecture<T> where T : Architecture<T> ,new()
    {
        private static T _architecture;

        static void MakeSureArchitecture()
        {
            if (_architecture == null)
            {
                _architecture = new T();
                _architecture.Init();
            }
        }

        protected abstract void Init();

        private IOCContainer _container = new IOCContainer();

        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return _architecture._container.Get<T>();
        }

        public void Register<T>(T instance)
        {
            MakeSureArchitecture();
            
            _architecture._container.Register<T>(instance);
        }
    }
}