using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour,IController
    {
        private void OnMouseDown()
        {
            gameObject.SetActive(false);
            this.SendCommand<KillEnemyCommand>();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Instance;
        }
    }
}