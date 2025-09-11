using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    None,
    Fire,
    Water,
    Ice,
    Electric,
    Poison,
    Paralysis
}

public enum DamageType { Slash, Blunt, Pierce, Explosion } // 참격, 타격, 관통, 폭발

[CreateAssetMenu(fileName = "BaseWeaponData", menuName = "Items/Equipment Data/Weapon")]
public class Weapon : BaseEquipmentData
{
    public WeaponAction WeaponAction;
    public float baseDamage;
    public ElementType element;
    public DamageType damageType;
    public float attackRange;           // 공격 판정 거리 (레이캐스트/콜라이더 길이)
    public float attackMoveSpeed;    // 공격 시 플레이어가 이동하는 속도
}
