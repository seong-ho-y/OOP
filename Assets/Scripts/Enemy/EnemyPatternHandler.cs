using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPatternHandler : MonoBehaviour
{
    private EnemyAI enemy;
    private List<Func<IEnumerator>> patternList;
    [SerializeField] private Transform player; // 플레이어 위치
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject aoePrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private float dashSpeed = 1f;
    [SerializeField] private float projectileSpeed = 0.7f;

    public void Initialize(EnemyAI ai)
    {
        enemy = ai;

        // 사용할 패턴들 등록
        patternList = new List<Func<IEnumerator>>
        {
            MeleeDashPattern,
            RangedProjectilePattern,
            AreaZonePattern
        };
    }

    public IEnumerator ExecuteRandomPattern()
    {
        int index = Random.Range(0, patternList.Count);
        yield return StartCoroutine(patternList[index].Invoke());
    }

    // 패턴 1: 돌진
    private IEnumerator MeleeDashPattern()
    {
        //Debug.Log("▶ 근거리 돌진 공격 실행 (아래 방향)");

        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + Vector2.down * 10f; // 아래로 3 유닛 돌진 (원하는 거리 조절 가능)

        // 1. 경고 영역 생성 및 위치 지정
        GameObject warningArea = Instantiate(warningPrefab);
        warningArea.transform.position = (startPos + targetPos) / 2f; // 시작점과 끝점 중간
        warningArea.transform.localScale = new Vector3(1.5f, Vector3.Distance(startPos, targetPos), 1f); // 폭 1.5, 높이는 거리만큼

        // 2. 경고 시간 대기 (예: 1.5초)
        yield return new WaitForSeconds(1.5f);

        // 3. 경고 제거
        Destroy(warningArea);

        // 4. 돌진 이동
        float dashTime = 0.4f;
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsed / dashTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; // 정확히 목표 위치에 도착하도록 보정

        //Debug.Log("돌진 완료!");
        transform.position = new Vector3(targetPos.x, 4f, 0f);
    }

    // 패턴 2: 원거리 발사
    private IEnumerator RangedProjectilePattern()
    {
        //Debug.Log("▶ 원거리 투사체 공격 실행");
        
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 dir = (player.position - transform.position).normalized;
        proj.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;

        yield return new WaitForSeconds(2f);
    }

    // 패턴 3: 장판 생성
    private IEnumerator AreaZonePattern()
    {
        //Debug.Log("▶ 장판 공격 실행 (경고 포함)");

        Vector2 targetPosition = player.position;

        // 1. 경고 영역 생성 (aoePrefab을 활용하거나 별도 경고 프리팹 사용 가능)
        GameObject warningArea = Instantiate(aoePrefab, targetPosition, Quaternion.identity);
    
        // 경고용으로 색이나 투명도 조절 (예: 빨간색 반투명)
        SpriteRenderer sr = warningArea.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0.5f; // 반투명
            sr.color = c;
        }
    
        // 2. 경고 시간 (예: 1.5초) 대기
        yield return new WaitForSeconds(0.5f);

        // 3. 경고 영역 제거
        Destroy(warningArea);

        // 4. 장판 생성 (실제 공격용)
        GameObject aoe = Instantiate(aoePrefab, targetPosition, Quaternion.identity);

        // 필요하다면 색 복원 등 추가

        // 5. 장판 지속 시간 대기 (예: 1초)
        yield return new WaitForSeconds(0.5f);

        // 6. 장판 제거
        Destroy(aoe);

        //Debug.Log("장판 공격 완료");
    }
}