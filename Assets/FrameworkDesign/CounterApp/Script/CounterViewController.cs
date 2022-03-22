using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterViewController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CounterModel.OnCountChanged += OnCountChanged;
        
        OnCountChanged(CounterModel.Count);
        
        transform.Find("BtnAdd").GetComponent<Button>().onClick.AddListener(() =>
        {
            CounterModel.Count++;
        });
        
        transform.Find("BtnSub").GetComponent<Button>().onClick.AddListener(() =>
        {
            CounterModel.Count--;
        });
    }
    

    private void OnDestroy()
    {
        CounterModel.OnCountChanged -= OnCountChanged;
    }

    private void OnCountChanged(int newCount)
    {
        transform.Find("CountText").GetComponent<Text>().text = newCount.ToString();
    }
}

public static class CounterModel
{
    private static int count;

    public static Action<int> OnCountChanged;
    public static int Count
    {
        get => count;
        set
        {
            if (value != count)
            {
                count = value;
                OnCountChanged?.Invoke(value);
            }
        }
    }
}
