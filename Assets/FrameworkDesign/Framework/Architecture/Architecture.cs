using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FrameworkDesign
{
    public interface IArchitecture
    {
        //注册system
        void RegisterSystem<T>(T system) where T : ISystem;
        //注册model
        void RegisterModel<T>(T model) where T : IModel;

        //注册utility
        void RegisterUtility<T>(T utility)where T : IUtility;

        T GetSystem<T>() where T : class, ISystem;
        T GetModel<T>() where T : class, IModel;
        
        T GetUtility<T>()where T : class, IUtility;

        void SendCommand<T>() where T : ICommand, new();
        void SendCommand<T>(T command) where T : ICommand;

        TResult SendQuery<TResult>(IQuery<TResult> query);

        void SendEvent<T>() where T : new();
        void SendEvent<T>(T e);

        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T> ,new()
    {
        private bool _inited= false;
        
        private List<IModel> _models = new List<IModel>();
        
        private List<ISystem> _systems = new List<ISystem>();

        public static Action<T> OnRegisterPatch = _architecture => { };
        
        private static T _architecture;

        public static IArchitecture Instance // TODO 考虑使用单例模板
        {
            get
            {
                if (_architecture == null)
                {
                    MakeSureArchitecture();
                }

                return _architecture;
            }
        }

        static void MakeSureArchitecture()
        {
            if (_architecture == null)
            {
                _architecture = new T();
                _architecture.Init();
                
                OnRegisterPatch?.Invoke(_architecture);
                
                foreach (var architectureModel in _architecture._models)
                {
                    architectureModel.Init();
                }
                _architecture._models.Clear();
                
                
                foreach (var system in _architecture._systems)
                {
                    system.Init();
                }
                _architecture._systems.Clear();
                
                
                _architecture._inited = true;
            }
        }

        //在子类的init里注册模块
        protected abstract void Init();

        private IOCContainer _container = new IOCContainer();

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.SetArchitecture(this);
            _container.Register<T>(system);

            if (!_inited)
            {
                _systems.Add(system);
            }
            else
            {
                system.Init();
            }
        }

        public void RegisterModel<T>(T model) where T : IModel
        {
            model.SetArchitecture(this);
            _container.Register<T>(model);

            if (!_inited)
            {
                _models.Add(model);
            }
            else
            {
                model.Init();
            }
        }

        public void RegisterUtility<T>(T utility)where T : IUtility
        {
            _container.Register<T>(utility);
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return _container.Get<T>();
        }

        public T GetModel<T>() where T : class, IModel
        {
            return _container.Get<T>();
        }

        public T GetUtility<T>() where T : class, IUtility
        {
            return _container.Get<T>();
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }

        private ITypeEventSystem _typeEventSystem = new TypeEventSystem();
        public void SendEvent<T>() where T : new()
        {
            _typeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            _typeEventSystem.Send<T>(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return _typeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            _typeEventSystem.UnRegister<T>(onEvent);
        }
    }
}