using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ButtonMergeBalls : ButtonController
{
    IDisposable _subject = null;

    protected override async UniTask Awake()
    {
        await base.Awake();
        _subject = GameManager.instance._canMerge.TakeUntilDestroy(this).Subscribe(x =>
                {
                    _btn.interactable = x;
                });

        act = () =>
        {
            BallObjPool.instance.MergeBalls();
        };
    }


    private void OnDestroy()
    {
        if (_subject != null)
            _subject.Dispose();
    }
}
