using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class CanDoEverything
    {
        public void DoSomething1()
        {
            Debug.Log("DoSomething1");
        }

        public void DoSomething2()
        {
            Debug.Log("DoSomething2");
        }

        public void DoSomething3()
        {
            Debug.Log("DoSomething3");
        }
    }

    public interface IHasEverything
    {
        CanDoEverything CanDoEverything { get;}
    }

    public interface ICanDoSomething1 : IHasEverything
    {
        
    }

    public static class ICanDoSomething1Extension
    {
        public static void DoSomething1(this ICanDoSomething1 selt)
        {
            selt.CanDoEverything.DoSomething1();
        }
    }
    public interface ICanDoSomething2 : IHasEverything
    {
        
    }

    public static class ICanDoSomething2Extension
    {
        public static void DoSomething2(this ICanDoSomething2 selt)
        {
            selt.CanDoEverything.DoSomething2();
        }
    }
    public interface ICanDoSomething3 : IHasEverything
    {
        
    }

    public static class ICanDoSomething3Extension
    {
        public static void DoSomething3(this ICanDoSomething3 selt)
        {
            selt.CanDoEverything.DoSomething3();
        }
    }
    
    
    
    
    public class InterfaceRuleExample : MonoBehaviour
    {
        public class OnlyCanDo1 : ICanDoSomething1
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }
        
        public class OnlyCanDo23 : ICanDoSomething2,ICanDoSomething3
        {
            CanDoEverything IHasEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        private void Start()
        {
            var onlyCanDo1 = new OnlyCanDo1();
            //onlyCanDo1.CanDoEverything.DoSomething1(); 报错，必须转换为IHasEverything去调用；  实现了
            onlyCanDo1.DoSomething1();
            
            var onlyCanDo23 = new OnlyCanDo23();
            onlyCanDo23.DoSomething2();
            onlyCanDo23.DoSomething3();
        }
    }
}