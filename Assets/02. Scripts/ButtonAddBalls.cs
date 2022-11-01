using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class ButtonAddBalls : ButtonController
{
    public int _startGrade = 1;
    IDisposable _subject;


    protected override async UniTask Awake()
    {
        await base.Awake();
        _subject = GameManager.instance._canAdd.TakeUntilDestroy(this).Subscribe(x =>
        {
            _btn.interactable = x;
        });
        act = () =>
        {
            BallObjPool.instance.Spawn(_startGrade);
        };
    }

    private void OnDestroy()
    {
        if (_subject != null)
            _subject.Dispose();
    }
}
