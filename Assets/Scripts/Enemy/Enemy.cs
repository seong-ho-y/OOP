// Enemy.cs
using UnityEngine;

public class Enemy : Creature // Creature 상속
{
    [Header("Enemy Specific")]
    public int expOnDeath = 10; // 사망 시 제공할 경험치

    public float deathDelay = 0f;
    
    [Header("Damage Text")]
    public GameObject damageTextPrefab; // Inspector에서 할당할 데미지 텍스트 프리팹
    public Transform damageTextSpawnPoint; // (선택 사항) 데미지 텍스트가 생성될 위치
    // --- 새로 추가할 부분 끝 ---
    protected override void Start()
    {
        base.Start(); // Creature의 Start() 호출
        CreatureName = gameObject.name; // 오브젝트 이름으로 적 이름 설정
        Debug.Log($"{CreatureName} appeared. Health: {CurrentHealth}/{MaxHealth}");
        // 적 AI 초기화 등
        if (damageTextSpawnPoint == null)
        {
            damageTextSpawnPoint = this.transform;
        }
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // 부모 클래스 (Creature)의 TakeDamage 로직 먼저 실행
        Debug.Log("전달할 데미지: " + damage);
        
        // --- 새로 추가할 부분 시작 ---
        // 데미지 텍스트 생성 및 표시 메서드 호출
        ShowDamageText(damage);
        // --- 새로 추가할 부분 끝 ---

        // (옵션) 적 피격 애니메이션 트리거
        // if (enemyAnimator != null) { enemyAnimator.SetTrigger("Hit"); }
        // (옵션) 적 피격 애니메이션 트리거
        // if (enemyAnimator != null) { enemyAnimator.SetTrigger("Hit"); }
    }
    // 대미지 팝업을 실제로 생성하고 설정하는 메서드
    
    void ShowDamageText(int damage)
    {
        // 1. damageTextPrefab이 Inspector에 제대로 할당되었는지 확인
        if (damageTextPrefab == null)
        {
            Debug.LogWarning("Enemy: damageTextPrefab이 할당되지 않았습니다! Inspector에서 설정해주세요.");
            return; // 프리팹이 없으면 더 이상 진행하지 않습니다.
        }

        // 2. 데미지 텍스트 프리팹 인스턴스화 (생성)
        // Instantiate(프리팹, 생성될 위치, 회전값, 부모 Transform)
        // UI 요소이므로 Canvas의 자식으로 만들어야 화면에 제대로 보입니다.
        // GameObject.FindObjectOfType<Canvas>()는 씬에 있는 첫 번째 Canvas를 찾아옵니다.
        // 만약 씬에 Canvas가 여러 개라면, 특정 Canvas를 Tag나 이름으로 찾아오는 것이 더 안전합니다.
        GameObject damageTextGO = Instantiate(damageTextPrefab, damageTextSpawnPoint.position, Quaternion.identity, GameObject.FindObjectOfType<Canvas>().transform);

        // 3. 생성된 데미지 텍스트 오브젝트에서 DamageTextController 컴포넌트 가져오기
        DamageTextController damageTextController = damageTextGO.GetComponent<DamageTextController>();

        // 4. 컴포넌트가 제대로 찾아졌는지 확인하고 데미지 값 설정
        if (damageTextController != null)
        {
            damageTextController.SetDamage(damage); // 데미지 값을 전달하여 텍스트를 업데이트하고 타이머 시작
        }
        else
        {
            Debug.LogError("Enemy: DamageTextPrefab에 DamageTextController 컴포넌트가 없습니다! 프리팹 설정을 확인해주세요.");
            // 프리팹에 스크립트가 붙어있지 않으면 이 오류가 발생합니다.
        }
    }
    // Creature의 추상 메서드 Die()를 구현 (오버라이드)
    public override void Die()
    {
        Debug.Log($"{CreatureName} has died.");
        // (옵션) 사망 애니메이션, 파티클 이펙트, 사운드 재생
        
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