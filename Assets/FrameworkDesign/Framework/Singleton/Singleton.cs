using System;
using System.Reflection;

namespace FrameworkDesign
{
    public class Singleton<T> where  T : Singleton<T>  //非常死的约束，使得无法像List<int> list 这样直接当作类型使用； 必须新建一个继承自Singleton<T>的类来使用
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