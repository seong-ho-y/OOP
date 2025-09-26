using System.Collections;
using UnityEngine;

public interface IWeaponAction
{
    IEnumerator Attack(PlayerAttack playerAttackRef, bool isCharged, float currentChargeTime = 0f, float maxChargeTime = 0f);
    void SwipeDown();
    void SwipeUp();
    void SwipeLeft();
    void SwipeRight();
}

public abstract class WeaponAction : ScriptableObject, IWeaponAction
{
    public abstract IEnumerator Attack(PlayerAttack playerAttackRef, bool isCharged, float currentChargeTime = 0,
        float maxChargeTime = 0);
    public abstract void SwipeDown();
    public abstract void SwipeUp();
    public abstract void SwipeLeft();
    public abstract void SwipeRight();
}