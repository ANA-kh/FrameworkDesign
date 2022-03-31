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

        //在子类的init里注册模块
        protected abstract void Init();

        private IOCContainer _container = new IOCContainer();

        //获取模块
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return _architecture._container.Get<T>();
        }

        //注册模块
        public void Register<T>(T instance)
        {
            MakeSureArchitecture();
            
            _architecture._container.Register<T>(instance);
        }
    }
}