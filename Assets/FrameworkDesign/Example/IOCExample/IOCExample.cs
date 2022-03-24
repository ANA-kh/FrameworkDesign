using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample : MonoBehaviour
    {
        private void Start()
        {
            var container = new IOCContainer();
            
            container.Register(new BluetoothManager());

            var bluetoorhManager = container.Get<BluetoothManager>();
            
            bluetoorhManager.Connect();
        }
    }

    public class BluetoothManager
    {
        public void Connect()
        {
            Debug.Log("蓝牙连接成功!");
        }
    }
}
