﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent); //返回注销对象，避免忘记注销
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public struct TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEventSystem TypeEventSystem;
        public Action<T> OnEvent;
        public void UnRegister()
        {
            TypeEventSystem.UnRegister<T>(OnEvent);
            TypeEventSystem = null;
            OnEvent = null;
        }
    }

    public class UnRegisterOnDestroyTrigger : MonoBehaviour //避免手动注销
    {
        private HashSet<IUnRegister> _unRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            _unRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in _unRegisters)
            {
                unRegister.UnRegister();
            }

            _unRegisters.Clear();
        }
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            if (!trigger) //MonoBehaviour重载了“！”  ？
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            
            trigger.AddUnRegister(unRegister);
        }
    }

    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {
            
        }
        
        public class Registrations<T> :IRegistrations
        {
            public Action<T> OnEvent = e => { };

        }
        
        //TODO 将Action的参数T（即注册的函数执行时的参数）的类型作为索引是否有问题？？？  
        private Dictionary<Type,IRegistrations> _eventRegistration = new Dictionary<Type, IRegistrations>();

        public void Send<T>() where T : new()
        {
            var e= new T();
            Send<T>(e);
        }

        public void Send<T>(T e)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (_eventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent(e);
            }
        }

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (_eventRegistration.TryGetValue(type, out registrations))
            {
                
            }
            else
            {
                registrations = new Registrations<T>();
                _eventRegistration.Add(type, registrations);
            }

            (registrations as Registrations<T>).OnEvent += onEvent;
            
            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent
            };
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;
            if (_eventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
    }
}