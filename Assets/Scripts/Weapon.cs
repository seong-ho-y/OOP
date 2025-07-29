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
    public float attackMoveSpeed;    // 공격 시 플레이어가 이동하는 속도

    public WeaponStats(float baseDamage, ElementType element, DamageType damageType, float attackRange, float attackMoveSpeed)
    {
        this.baseDamage = baseDamage;
        this.element = element;
        this.damageType = damageType;
        this.attackRange = attackRange;
        this.attackMoveSpeed = attackMoveSpeed;
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
