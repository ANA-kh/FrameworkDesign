using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour,IController
    {
        private void Awake()
        {
            this.RegisterEvent<GamePassEvent>(OnGameEnd);
        }

        private void OnGameEnd(GamePassEvent e)
        {
            transform.Find("Canvas/GameStopPanel").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GamePassEvent>(OnGameEnd);
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Instance;
        }
    }
}