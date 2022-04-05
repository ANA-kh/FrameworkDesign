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
            _counterModel = GetArchitecture().GetModel<ICounterModel>();
            _counterModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(_counterModel.Count.Value);

            transform.Find("BtnAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                GetArchitecture().SendCommand<AddCountCommand>();
            });

            transform.Find("BtnSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                GetArchitecture().SendCommand<SubCountCommand>();
            });
        }


        private void OnDestroy()
        {
            _counterModel.Count.OnValueChanged -= OnCountChanged;
            _counterModel = null;
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.Instance;
        }
    }

    public class OnCountChangedEvent : Event<OnCountChangedEvent>
    {

    }

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : AbstractModel,ICounterModel
    {
        protected override void OnInit()
        {
            var storage = GetArchitecture().GetUtility<IStorage>();//TODO 考虑使用接口注入
            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);

            Count.OnValueChanged += count =>
            {
                storage.SaveInt("COUNTER_COUNT", count);
            };
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}