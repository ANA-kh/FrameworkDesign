using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class TypeEventSystemExample : MonoBehaviour
    {
        public struct EventA
        {
            
        }
        
        public struct EventB
        {
            public int ParamB;
        }
        
        public interface IEventGroup
        {
            
        }
        
        public struct EventC : IEventGroup
        {
            
        }
        
        public struct EventD : IEventGroup
        {
            
        }

        private TypeEventSystem _typeEventSystem = new TypeEventSystem();
        private void Start()
        {
            _typeEventSystem.Register<EventA>(OnEventA);//需要手动注销
            _typeEventSystem.Register<EventB>(b =>
            {
                Debug.Log("OnEventB:" + b.ParamB);
            }).UnRegisterWhenGameObjectDestroyed(gameObject); //当前gameObject销毁时，自动注销
            
            _typeEventSystem.Register<IEventGroup>( e =>
            {
                Debug.Log(e.GetType());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnEventA(EventA obj)
        {
            Debug.Log("OnEventA");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _typeEventSystem.Send<EventA>();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _typeEventSystem.Send(new EventB(){ParamB = 123});
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _typeEventSystem.Send<IEventGroup>(new EventC());
                _typeEventSystem.Send<IEventGroup>(new EventD());
            }
        }

        private void OnDestroy()
        {
            _typeEventSystem.UnRegister<EventA>(OnEventA);
            _typeEventSystem = null;
        }
    }
}