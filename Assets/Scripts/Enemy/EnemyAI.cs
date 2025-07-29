using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    // 이동 범위 설정 (임시값: x: -1.7 ~ 1.7, y: 1.5 ~ 4.5)
    [SerializeField] private Vector2 xRange = new Vector2(-1.7f, 1.7f);
    [SerializeField] private Vector2 yRange = new Vector2(1.5f, 4.5f);

    private Vector3 targetPosition;
    
    public EnemyPatternHandler patternHandler;

    private bool isAttacking = false;

    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(RandomMoveRoutine());
        patternHandler.Initialize(this);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        if (!isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }
    private IEnumerator RandomMoveRoutine()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                SetNewTargetPosition();
            }

            yield return new WaitForSeconds(0.1f); // 체크 주기
        }
    }

    private void SetNewTargetPosition()
    {
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomY = Random.Range(yRange.x, yRange.y);
        targetPosition = new Vector3(randomX, randomY, 0f);
    }
    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        yield return patternHandler.ExecuteRandomPattern();

        isAttacking = false;
        yield return new WaitForSeconds(4.0f); // 공격 간 딜레이
    }
}
