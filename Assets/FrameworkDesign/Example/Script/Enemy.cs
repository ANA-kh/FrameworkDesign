using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject GamePassPanel;
        private static int mKilledEnemyCount = 0; 
        private void OnMouseDown()
        {
            Destroy(gameObject);

            mKilledEnemyCount++;

            if (mKilledEnemyCount == 4)
            {
                GameEndEvent.Trigger();
            }
        }
    }
}