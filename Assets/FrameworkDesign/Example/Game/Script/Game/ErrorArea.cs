using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class ErrorArea : MonoBehaviour, IController
    {
        private void OnMouseDown()
        {
            Debug.Log("点错了");
            this.SendCommand<MissCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Instance;
        }
    }
}