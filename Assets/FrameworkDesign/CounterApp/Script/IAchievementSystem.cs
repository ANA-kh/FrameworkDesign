using FrameworkDesign;
using UnityEngine;

namespace CounterApp
{
    public interface IAchievementSystem : ISystem
    {
        
    }
    
    public class AchievementSystem : IAchievementSystem
    {
        public IArchitecture Architecture { get; set; }
        public void Init()
        {
            var counterModel = Architecture.GetModel<ICounterModel>();

            var previousCount = counterModel.Count.Value;

            counterModel.Count.OnValueChanged += newCount =>
            {
                if (previousCount < 10 && newCount >= 10)
                {
                    Debug.Log("--10");//发送解锁成就事件到表现层
                }
                else if(previousCount < 20 && newCount >= 20)
                {
                    Debug.Log("--20");
                }

                previousCount = newCount;
            };
        }
    }
}