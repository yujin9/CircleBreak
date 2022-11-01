using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ball"))
        {
            var ball = collision.gameObject.GetComponent<BallController>();

            Vector2 income = ball.MovePos; // 입사벡터
            Vector2 normal = collision.contacts[0].normal; // 법선벡터
            ball.MovePos = Vector2.Reflect(income, normal).normalized; // 반사벡터

        }
    }
}
