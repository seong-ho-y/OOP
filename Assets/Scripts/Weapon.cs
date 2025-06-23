using UnityEngine;

public class WeaponStats
{
    public float baseDamage;
    public string damage;
    public string element;
    public string damageType;

    public WeaponStats(float baseDamage, string damage, string element, string damageType)
    {
        this.baseDamage = baseDamage;
        this.damage = damage;
        this.element = element;
        this.damageType = damageType;
    }
}
public interface IWeaponAction
{
    void Attack();
    void SwipeDown();
    void SwipeUp();
    void SwipeLeft();
    void SwipeRight();
}

public class GreatSword : IWeaponAction
{
    public virtual void Attack()
    {
        Debug.Log("모아베기");
    }

    public virtual void SwipeDown()
    {
        Debug.Log("가드");
    }

    public virtual void SwipeUp()
    {
        Debug.Log("모으기 강화");
    }

    public virtual void SwipeLeft()
    {
        Debug.Log("왼쪽 구르기");
    }

    public virtual void SwipeRight()
    {
        Debug.Log("오른쪽 구르기");
    }
}

