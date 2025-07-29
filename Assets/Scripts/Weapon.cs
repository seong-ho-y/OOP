using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { None, Fire, Water, Ice, Thunder, Dragon }
public enum DamageType { Slash, Blunt, Pierce, Explosion } // 참격, 타격, 관통, 폭발

public class WeaponStats
{
    public float baseDamage;
    public ElementType element;
    public DamageType damageType;
    public float attackRange;           // 공격 판정 거리 (레이캐스트/콜라이더 길이)
    public float attackMoveDistance;    // 공격 시 플레이어가 전진할 거리 (예: 2m)
    public float attackMoveDuration;    // 공격 전진에 걸리는 시간 (예: 0.2초) - 이 값이 속도를 결정

    public WeaponStats(float baseDamage, ElementType element, DamageType damageType, float attackRange, float attackMoveDistance, float attackMoveDuration)
    {
        this.baseDamage = baseDamage;
        this.element = element;
        this.damageType = damageType;
        this.attackRange = attackRange;
        this.attackMoveDistance = attackMoveDistance;
        this.attackMoveDuration = attackMoveDuration;
    }
}
public interface IWeaponAction
{
    IEnumerator Attack(PlayerAttack playerAttackRef, bool isCharged, float currentChargeTime = 0f, float maxChargeTime = 0f);
    void SwipeDown();
    void SwipeUp();
    void SwipeLeft();
    void SwipeRight();
}
