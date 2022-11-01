using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

public class ButtonIncreaseBallCount : ButtonController
{
    public Text _text;

    IDisposable _fullCntBall;
    IDisposable _curCntBall;
    
    protected override async UniTask Awake()
    {
        await base.Awake();
        _fullCntBall = GameManager.instance._ballFullCount.TakeUntilDestroy(this).Subscribe(x =>
                {
                    _text.text = $"{GameManager.instance._curBallCount.Value} / {x}";
                });
        _curCntBall = GameManager.instance._curBallCount.TakeUntilDestroy(this).Subscribe(x =>
        {
            _text.text = $"{x} / {GameManager.instance._ballFullCount.Value}";
            if (x == GameManager.instance._ballFullCount.Value)
            {
                GameManager.instance._canAdd.Value = false;
            }
            else
            {
                GameManager.instance._canAdd.Value = true;
            }
        });

        act = IncreaseFullCount;
    }


    private void OnDestroy()
    {
        if (_fullCntBall != null) _fullCntBall.Dispose();
        if (_curCntBall != null) _curCntBall.Dispose();
    }

    private void IncreaseFullCount()
    {
        GameManager.instance._ballFullCount.Value++;
        if (GameManager.instance._canAdd.Value == false)
            GameManager.instance._canAdd.Value = true;
    }

}
