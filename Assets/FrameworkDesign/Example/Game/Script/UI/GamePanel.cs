using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GamePanel : MonoBehaviour, IController
    {
        private ICountDownSystem _countDownSystem;
        private IGameModel _gameModel;

        private void Awake()
        {
            _countDownSystem = this.GetSystem<ICountDownSystem>();

            _gameModel = this.GetModel<IGameModel>();

            _gameModel.Gold.Register(OnGoldValueChanged);
            _gameModel.Life.Register(OnLifeValueChanged);
            _gameModel.Score.Register(OnScoreValueChanged);

            // 第一次需要调用一下
            OnGoldValueChanged(_gameModel.Gold.Value);
            OnLifeValueChanged(_gameModel.Life.Value);
            OnScoreValueChanged(_gameModel.Score.Value);
        }

        private void OnLifeValueChanged(int life)
        {
            transform.Find("LifeText").GetComponent<Text>().text = "生命：" + life;
        }

        private void OnGoldValueChanged(int gold)
        {
            transform.Find("GoldText").GetComponent<Text>().text = "金币：" + gold;
        }

        private void OnScoreValueChanged(int score)
        {
            transform.Find("ScoreText").GetComponent<Text>().text = "分数:" + score;
        }

        private void Update()
        {
            // 每 20 帧 更新一次
            if (Time.frameCount % 20 == 0)
            {
                transform.Find("CountDownText").GetComponent<Text>().text =
                    _countDownSystem.CurrentRemainSeconds + "s";

                _countDownSystem.Update();
            }
        }

        private void OnDestroy()
        {
            _gameModel.Gold.UnRegister(OnGoldValueChanged);
            _gameModel.Life.UnRegister(OnLifeValueChanged);
            _gameModel.Score.UnRegister(OnScoreValueChanged);
            _gameModel = null;
            _countDownSystem = null;
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Instance;
        }
    }

}