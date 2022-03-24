using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour
    {
        private void Awake()
        {
            GamePassEvent.Register(OnGameEnd);
        }

        private void OnGameEnd()
        {
            transform.Find("Canvas/GameStopPanel").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            GamePassEvent.UnRegister(OnGameEnd);
        }
    }
}