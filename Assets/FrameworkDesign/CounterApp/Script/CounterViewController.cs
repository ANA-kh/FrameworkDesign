using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CounterModel.Instance.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(CounterModel.Instance.Count.Value);

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
            CounterModel.Instance.Count.OnValueChanged -= OnCountChanged;
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
        }
    }

    public class OnCountChangedEvent : Event<OnCountChangedEvent>
    {

    }

    public class CounterModel : Singleton<CounterModel>
    {
        private CounterModel(){}
        public BindableProperty<int> Count = new BindableProperty<int>()
        {
            Value = 0
        };
    }
}