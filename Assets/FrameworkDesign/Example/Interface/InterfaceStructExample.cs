using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class InterfaceStructExample : MonoBehaviour
    {
        public interface ICustomScript
        {
            void Start();
            void Update();
            void Destroy();
        }
        
        public abstract class CustomScript : ICustomScript
        {
            void ICustomScript.Start()
            {
                OnStart();
            }

            void ICustomScript.Update()
            {
                OnUpdate();
            }

            void ICustomScript.Destroy()
            {
                OnDestroy();
            }

            public abstract void OnStart();
            public abstract void OnUpdate();
            public abstract void OnDestroy();
        }
        
        public class MyScript : CustomScript
        {
            public override void OnStart()
            {
                //Start();会造成死循环   使用explicit interface使得Start()无法直接调用
                Debug.Log("OnStart");
            }

            public override void OnUpdate()
            {
                Debug.Log("OnUpdate");
            }

            public override void OnDestroy()
            {
                Debug.Log("OnDestroy");
            }
        }

        private void Start()
        {
            ICustomScript myScript = new MyScript();
            myScript.Start();
            myScript.Update();
            myScript.Destroy();
        }
    }
}