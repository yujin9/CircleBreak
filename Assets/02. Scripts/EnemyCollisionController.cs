using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyCollisionController : CollisionController
{
    private Vector3 _originPos;
    private Sequence mySequence;

    private void Awake()
    {
        _originPos = transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine(CoRotate());
    }


    IEnumerator CoRotate()
    {
        while (true)
        {
            var angles = transform.localRotation.eulerAngles;
            angles.y -= Time.deltaTime * 30f;
            transform.localRotation = Quaternion.Euler(angles);
            yield return null;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        //transform.DOShakePosition(0.03f, 0.25f);
        //transform.position = _originPos;

        transform.DOScale(3.3f, 0.05f);
        yield return new WaitForSeconds(0.05f);
        transform.DOScale(3f, 0.05f);
    }
}
