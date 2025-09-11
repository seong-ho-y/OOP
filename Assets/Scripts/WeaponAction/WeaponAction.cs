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
    public IEnumerator Attack(PlayerAttack playerAttackRef, bool isCharged, float currentChargeTime = 0, float maxChargeTime = 0)
    {
        throw new System.NotImplementedException();
    }

    public void SwipeDown()
    {
        throw new System.NotImplementedException();
    }

    public void SwipeUp()
    {
        throw new System.NotImplementedException();
    }

    public void SwipeLeft()
    {
        throw new System.NotImplementedException();
    }

    public void SwipeRight()
    {
        throw new System.NotImplementedException();
    }
}