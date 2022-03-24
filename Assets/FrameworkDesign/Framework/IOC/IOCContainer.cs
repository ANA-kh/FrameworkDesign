using System;
using System.Collections.Generic;

namespace FrameworkDesign
{
    public class IOCContainer
    {
        Dictionary<Type,object> instances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (instances.ContainsKey(key))
            {
                instances[key] = instance;
            }
            else
            {
                instances.Add(key,instance);
            }
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);
            if (instances.TryGetValue(key, out var retInstance))
            {
                return retInstance as T;
            }
            
            return null;
        }
    }
}