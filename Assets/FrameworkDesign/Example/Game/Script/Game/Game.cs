using System; 
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour,IController
    {
        private void Awake()
        {
            this.RegisterEvent<GameStartEvent>(OnGameStart);
        }


        private void OnGameStart(GameStartEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GameStartEvent>(OnGameStart);
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Instance;
        }
    }
}