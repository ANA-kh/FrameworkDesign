using System;

namespace FrameworkDesign.Example
{
    public interface ICountDownSystem :ISystem
    {
        int CurrentRemainSeconds { get; }

        void Update();
    }
    
    public class CountDownSystem : AbstractSystem ,ICountDownSystem
    {
        private bool _start =false;
        private DateTime _gameStartTime { get; set; }

        protected override void OnInit()
        {
            this.RegisterEvent<GameStartEvent>(e =>
            {
                _start = true;
                _gameStartTime = DateTime.Now;
            });

            this.RegisterEvent<GamePassEvent>(e =>
            {
                _start = false;
            });
        }

        public int CurrentRemainSeconds => 10 - (int) (DateTime.Now - _gameStartTime).TotalSeconds;
        
        public void Update()
        {
            if (_start)
            {
                if(DateTime.Now - _gameStartTime > TimeSpan.FromSeconds(10))
                {
                    this.SendEvent<OnCountDownEndEvent>();
                    _start = false;
                }
            }
        }
    }
}