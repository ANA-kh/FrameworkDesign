using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private IGameModel _gameModel;

        void Start()
        {
            transform.Find("BtnGameStart").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);

                    this.SendCommand<StartGameCommand>();
                });

            transform.Find("BtnBuyLife").GetComponent<Button>()
                .onClick.AddListener(this.SendCommand<BuyLifeCommand>);

            _gameModel = this.GetModel<IGameModel>();

            _gameModel.Gold.Register(OnGoldValueChanged);
            _gameModel.Life.Register(OnLifeValueChanged);

            // 第一次需要调用一下
            OnGoldValueChanged(_gameModel.Gold.Value);
            OnLifeValueChanged(_gameModel.Life.Value);

            transform.Find("BestScoreText").GetComponent<Text>().text = "最高分:" + _gameModel.BestScore.Value;
        }

        private void OnEnable()
        {
            transform.Find("BestScoreText").GetComponent<Text>().text = "最高分:" + _gameModel.BestScore.Value;
        }

        private void OnLifeValueChanged(int life)
        {
            transform.Find("LifeText").GetComponent<Text>().text = "生命：" + life;
        }

        private void OnGoldValueChanged(int gold)
        {
            if (gold > 0)
            {
                transform.Find("BtnBuyLife").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("BtnBuyLife").gameObject.SetActive(false);
            }

            transform.Find("GoldText").GetComponent<Text>().text = "金币：" + gold;
        }


        private void OnDestroy()
        {
            _gameModel.Gold.UnRegister(OnGoldValueChanged);
            _gameModel.Life.UnRegister(OnLifeValueChanged);
            _gameModel = null;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Instance;
        }
    }
}