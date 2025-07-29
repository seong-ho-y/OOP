// Enemy.cs
using UnityEngine;

public class Enemy : Creature // Creature 상속
{
    [Header("Enemy Specific")]
    public int expOnDeath = 10; // 사망 시 제공할 경험치

    protected override void Start()
    {
        base.Start(); // Creature의 Start() 호출
        CreatureName = gameObject.name; // 오브젝트 이름으로 적 이름 설정
        Debug.Log($"{CreatureName} appeared. Health: {CurrentHealth}/{MaxHealth}");
        // 적 AI 초기화 등
    }

    public override void Die() // abstract 메서드이므로 반드시 override
    {
        Debug.Log($"{CreatureName} died. Earned {expOnDeath} EXP.");
        // 적 사망 애니메이션, 아이템 드랍, 파티클 효과
        // GameManager.Instance.AddExp(expOnDeath); // 경험치 추가 (가정)
        Destroy(gameObject); // 적 오브젝트 파괴
    }

    // 적 고유의 AI 행동 패턴, 공격 로직 등은 여기에 구현
    // public override void Attack(int damageAmount, Creature targetCreature)
    // {
    //    // 적 고유의 공격 패턴 로직 구현 후 base.Attack 호출 또는 완전히 새로운 로직 구현
    //    base.Attack(damageAmount, targetCreature);
    // }
}