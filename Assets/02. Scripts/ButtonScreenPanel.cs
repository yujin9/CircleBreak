using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ButtonScreenPanel : ButtonController
{
    Coroutine _coAct;
    float originSpeed;
    protected override async UniTask Awake()
    {
        await base.Awake();
        act = SpeedUp;
    }

    private void Start()
    {
        originSpeed = GameManager.instance._ballSpeed;
    }

    private void SpeedUp()
    {
        StartCoroutine(CoSpeedUp());
    }

    private IEnumerator CoSpeedUp()
    {
        if (GameManager.instance._ballSpeed == originSpeed && BallObjPool.instance._spawnBallList.Count > 0)
        {
            GameManager.instance._ballSpeed *= 2f;
            GameManager.instance.isSpdUp.Value = true;
            
            yield return new WaitForSeconds(0.5f);
            GameManager.instance._ballSpeed = originSpeed;
            GameManager.instance.isSpdUp.Value = false;
        }
    }

    
}
