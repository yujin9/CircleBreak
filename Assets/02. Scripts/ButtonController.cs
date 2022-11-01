using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] protected Button _btn;

    protected Action act;
    protected virtual async UniTask Awake()
    {
        await UniTask.WaitUntil(() => GameManager.instance != null);

        _btn.onClick.AddListener(() =>
        {
            act?.Invoke();
        });
    }
}
