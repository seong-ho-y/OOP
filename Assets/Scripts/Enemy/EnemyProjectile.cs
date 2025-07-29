using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float lifeTime = 5f;          // 보호용 수명
    [SerializeField] private int   damage   = 1;           // 플레이어 데미지

    private void Start()
    {
        // 중력·회전 영향 제거(필요에 따라 조정)
        var rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // 일정 시간이 지나면 자동 삭제
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ① 플레이어에 맞았을 때
        if (other.CompareTag("Player"))
        {
            // 예시: Player 스크립트에 TakeDamage(int) 가 있다고 가정
            other.GetComponent<Player>()?.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // ② 레일(또는 바닥·벽) 충돌 시
        //if (other.CompareTag("Rail") || other.CompareTag("Obstacle"))
        //{
         //   Destroy(gameObject);
        //}
    }
}