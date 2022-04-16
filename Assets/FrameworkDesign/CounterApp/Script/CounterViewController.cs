using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour, IController
    {
        private ICounterModel _counterModel;
        void Start()
        {
            _counterModel = this.GetModel<ICounterModel>();
            _counterModel.Count.RegisterOnValueChanged(OnCountChanged);
            OnCountChanged(_counterModel.Count.Value);

            transform.Find("BtnAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<AddCountCommand>();
            });

            transform.Find("BtnSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<SubCountCommand>();
            });
        }


        private void OnDestroy()
        {
            _counterModel.Count.RegisterOnValueChanged(OnCountChanged);
            _counterModel = null;
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Instance;
        }
    }
    

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : AbstractModel,ICounterModel
    {
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();//TODO 考虑使用接口注入
            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);

            Count.RegisterOnValueChanged( count =>
            {
                storage.SaveInt("COUNTER_COUNT", count);
            });
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}