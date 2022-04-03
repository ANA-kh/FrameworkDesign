using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        private ICounterModel _counterModel;
        void Start()
        {
            _counterModel = CounterApp.Get<ICounterModel>();
            _counterModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(_counterModel.Count.Value);

            transform.Find("BtnAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                new AddCountCommand().Execute();
            });

            transform.Find("BtnSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                new SubCountCommand().Execute();
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
    }

    public class OnCountChangedEvent : Event<OnCountChangedEvent>
    {

    }

    public interface ICounterModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : ICounterModel
    {
        

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}