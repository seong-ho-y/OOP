// Enemy.cs
using UnityEngine;

public class Enemy : Creature // Creature 상속
{
    [Header("Enemy Specific")]
    public int expOnDeath = 10; // 사망 시 제공할 경험치

    public float deathDelay = 1f;
    
    [Header("Damage Pop Up Settings")]
    public GameObject damagePopUpPrefab; // 인스펙터에서 DamagePopUp 프리팹 할당
    public Vector3 popUpOffset = new Vector3(0, 1.5f, 0); // 몬스터 머리 위로 띄울 오프셋
    protected override void Start()
    {
        base.Start(); // Creature의 Start() 호출
        CreatureName = gameObject.name; // 오브젝트 이름으로 적 이름 설정
        Debug.Log($"{CreatureName} appeared. Health: {CurrentHealth}/{MaxHealth}");
        // 적 AI 초기화 등
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // 부모 클래스 (Creature)의 TakeDamage 로직 먼저 실행

        // 피격 시 대미지 팝업 표시 (여기서 호출!)
        ShowDamagePopUp(damage); // 최종 데미지 말고, 인풋 데미지를 넘겨줘도 좋음 (finalDamage로 넘겨도 됨)

        // (옵션) 적 피격 애니메이션 트리거
        // if (enemyAnimator != null) { enemyAnimator.SetTrigger("Hit"); }
    }
    // 대미지 팝업을 실제로 생성하고 설정하는 메서드
    private void ShowDamagePopUp(int damageAmount)
    {
        if (damagePopUpPrefab != null)
        {
            // 몬스터 위치 + 오프셋에 대미지 팝업 생성
            // 팁: 오브젝트 풀링을 사용하면 Instantiate/Destroy 오버헤드를 줄일 수 있습니다.
            GameObject popUpGO = Instantiate(damagePopUpPrefab, transform.position + popUpOffset, Quaternion.identity);
            
            // DamagePopUp 스크립트 참조를 가져와서 대미지 값 설정
            DamagePopUp popUpScript = popUpGO.GetComponent<DamagePopUp>();
            if (popUpScript != null)
            {
                popUpScript.SetDamageText(damageAmount);
            }
            else
            {
                Debug.LogWarning("DamagePopUp prefab does not have DamagePopUp script attached!");
            }
        }
        else
        {
            Debug.LogWarning("Damage Pop Up Prefab is not assigned in the Inspector for " + CreatureName + "!");
        }
    }

    // Creature의 추상 메서드 Die()를 구현 (오버라이드)
    public override void Die()
    {
        Debug.Log($"{CreatureName} has died.");
        // (옵션) 사망 애니메이션, 파티클 이펙트, 사운드 재생

        // 일정 시간 후 오브젝트 파괴 또는 비활성화
        // 예: Invoke("DestroyEnemy", deathDelay);
        Destroy(gameObject, deathDelay); // deathDelay 시간 후에 게임 오브젝트 파괴
    }

    // 사망 애니메이션 등이 끝난 후 실제 파괴를 위한 메서드 (Invoke 사용 시)
    // private void DestroyEnemy()
    // {
    //     Destroy(gameObject);
    // }

    // 적 고유의 AI 행동 패턴, 공격 로직 등은 여기에 구현
    // public override void Attack(int damageAmount, Creature targetCreature)
    // {
    //    // 적 고유의 공격 패턴 로직 구현 후 base.Attack 호출 또는 완전히 새로운 로직 구현
    //    base.Attack(damageAmount, targetCreature);
    // }
}