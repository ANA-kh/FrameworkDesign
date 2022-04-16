using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CounterApp;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IAchievementSystem : ISystem
    {
        
    }

    public class AchievementItem
    {
        public string Name { get; set; }
        public Func<bool> CheckComplete { get; set; }
        public bool Unlocked { get; set; }
    }
    
    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private List<AchievementItem> _items = new List<AchievementItem>();

        private bool _missed = false;
        protected override void OnInit()
        {
            this.RegisterEvent<OnMissEvent>(e =>
            {
                _missed = true;
            });

            this.RegisterEvent<GameStartEvent>(e =>
            {
                _missed = false;
            });
            
            _items.Add(new AchievementItem()
            {
                Name = "百分成就",
                CheckComplete = () => this.GetModel<IGameModel>().BestScore.Value > 100
            });
            
            _items.Add(new AchievementItem()
            {
                Name = "手残",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value < 0
            });

            _items.Add(new AchievementItem()
            {
                Name = "零失误成就",
                CheckComplete = () => !_missed
            });
            
            _items.Add(new AchievementItem()
            {
                Name = "零失误成就",
                CheckComplete = () => _items.Count(item => item.Unlocked) >= 3
            });
            
            this.RegisterEvent<GamePassEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
                
                foreach (var achievementItem in _items)
                {
                    if (!achievementItem.Unlocked && achievementItem.CheckComplete())
                    {
                        achievementItem.Unlocked = true;

                        Debug.Log("解锁 成就:" + achievementItem.Name);
                    }
                }
            });

        }
    }
}