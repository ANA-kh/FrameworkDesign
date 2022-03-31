namespace FrameworkDesign.Example
{
    public class PointGame
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
            _container.Register(new GameModel());
        }

        public static T Get<T>() where T : class
        {
            MakeSureContainer();
            return _container.Get<T>();
        }
    }
}