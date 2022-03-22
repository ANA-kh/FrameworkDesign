using System; 
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            GameStartEvent.Register(OnGameStart);
            KilledOneEnemyEvent.Register(OnEnemyKilled);
        }

        private void OnEnemyKilled()
        {
            GameModel.KillCount++;
            if (GameModel.KillCount == 4)
            {
                GamePassEvent.Trigger();
            }
        }

        private void OnGameStart()
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            GameStartEvent.UnRegister(OnGameStart);
            KilledOneEnemyEvent.UnRegister(OnEnemyKilled);
        }
    }
}