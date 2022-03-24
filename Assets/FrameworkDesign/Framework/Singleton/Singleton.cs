using System;
using System.Reflection;

namespace FrameworkDesign
{
    public class Singleton<T> where  T : Singleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var type = typeof(T);
                    var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    var ctor = Array.Find(constructors, x => x.GetParameters().Length == 0);
                    instance = ctor.Invoke(null) as T;
                }
                return instance;
            }
            set => instance = value;
        }
    }
}