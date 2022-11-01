using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ButtonRVButton : ButtonController
{
    private float _originSpeed;

    protected override async UniTask Awake()
    {
        await base.Awake();

        act = SpeedUp;
    }

    private void SpeedUp()
    {
        StartCoroutine(RVSpeedUp());
    }

    void Start()
    {
        _originSpeed = GameManager.instance._ballSpeed;
    }


    private IEnumerator RVSpeedUp()
    {
        if (GameManager.instance._ballSpeed == _originSpeed && BallObjPool.instance._spawnBallList.Count > 0)
        {
            GameManager.instance._ballSpeed *= 3f;
            GameManager.instance.isSpdUp.Value = true;
            _btn.interactable = false;
            yield return new WaitForSeconds(3f);
            GameManager.instance._ballSpeed = _originSpeed;
            GameManager.instance.isSpdUp.Value = false;
            _btn.interactable = true;
        }
    }
}
