using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public IntReactiveProperty _ballFullCount = new IntReactiveProperty(3);
    public BoolReactiveProperty _canMerge = new BoolReactiveProperty(false);
    public BoolReactiveProperty _canAdd = new BoolReactiveProperty(true);
    public IntReactiveProperty _curBallCount = new IntReactiveProperty(0);
    public FloatReactiveProperty _currentCurrency = new FloatReactiveProperty(0f);
    public BoolReactiveProperty isSpdUp = new BoolReactiveProperty(false);

    public float _ballSpeed;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        _currentCurrency.TakeUntilDestroy(this).Subscribe(x =>
        {
            //재화 변동시 행동할 이벤트

        });
    }

    public void IncreaseCurrency(float currencyValue)
    {
        _currentCurrency.Value += currencyValue;
    }
    public void DecreaseCurrency(float currencyValue)
    {
        _currentCurrency.Value -= currencyValue;
    }
}
