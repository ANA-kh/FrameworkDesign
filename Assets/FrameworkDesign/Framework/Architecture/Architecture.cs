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
        void RegisterUtility<T>(T utility);

        T GetModel<T>() where T : class, IModel;
        
        T GetUtility<T>()where T : class;
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T> ,new()
    {
        private bool _inited= false;
        
        private List<IModel> _models = new List<IModel>();
        
        private List<ISystem> _systems = new List<ISystem>();

        public static Action<T> OnRegisterPatch = _architecture => { };
        
        private static T _architecture;

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

        //获取模块
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return _architecture._container.Get<T>();
        }

        //注册模块
        //TODO 内部用RegisterUtility，外部用Register。  相同功能，易混淆不太好
        public static void Register<T>(T instance)
        {
            MakeSureArchitecture();
            
            _architecture._container.Register<T>(instance);
        }

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.Architecture = this;
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
            model.Architecture = this;
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

        public void RegisterUtility<T>(T utility)
        {
            _container.Register<T>(utility);
        }

        public T GetModel<T>() where T : class, IModel
        {
            return _container.Get<T>();
        }

        public T GetUtility<T>() where T : class
        {
            return _container.Get<T>();
        }
    }
}