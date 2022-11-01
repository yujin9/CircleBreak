using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollisionController : CollisionController
{
    private Coroutine _co;
    private Vector3 _originPos;

    private void Awake()
    {
        _originPos = transform.position;
    }

    private void OnEnable()
    {
        _co = StartCoroutine(CoRotate());
    }


    IEnumerator CoRotate()
    {
        while (true)
        {
            var angles = transform.rotation.eulerAngles;
            angles.y -= Time.deltaTime * 30f;
            transform.rotation = Quaternion.Euler(angles);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_co);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        transform.DOShakePosition(0.03f, 0.25f);
        transform.position = _originPos;
        yield return new WaitUntil(() => transform.position == _originPos);
    }
}
