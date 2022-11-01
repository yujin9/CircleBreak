using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [HideInInspector] public Vector3 MovePos; // 이동벡터
    public MeshRenderer _ballMesh;
    public TrailRenderer _trail;
    public int _grade;
    public int _index;
    public float _multiply;
    private Coroutine _curCoroutine;


    public void StartBall()
    {
        MovePos = new Vector2(Random.Range(0.2f, 0.45f), Random.Range(0f, 0.5f)).normalized;

        _curCoroutine = StartCoroutine(CoMove());

    }

    public void InitializeBall()
    {
        MovePos = Vector2.zero;

        StopCoroutine(_curCoroutine);
    }

    IEnumerator CoMove()
    {
        while (true)
        {
            transform.position += MovePos * GameManager.instance._ballSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
