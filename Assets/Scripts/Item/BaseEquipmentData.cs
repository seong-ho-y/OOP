using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Chest,
    Arms,
    Waist,
    Legs,
    Weapon,
    Stone
}
public abstract class BaseEquipmentData : BaseItemData, IEquipable, IUpgradable
{
    [Header("장비 타입")]
    public EquipmentSlot equipSlot;
    
    [Header("업그레이드 정보")]
    public List<MaterialCost> requiredMaterials; // 업그레이드 필요 재료
    
    public void Equip() {}
    public void UnEquip() {}

    // IUpgradable 인터페이스 구현
    public bool CanUpgrade(Dictionary<string, int> playerMaterials)
    {
        foreach (var material in requiredMaterials)
        {
            if (!playerMaterials.ContainsKey(material.MaterialItemData.itemName) || playerMaterials[material.MaterialItemData.itemName] < material.cost)
            {
                return false; // 재료가 부족하면 false
            }
        }
        return true;
    }

    public void Upgrade()
    {
        Debug.Log("장비 강화 성공");
    }
}
public interface IEquipable
{
    public void Equip();
    public void UnEquip();
}

public interface IUpgradable
{
    bool CanUpgrade(Dictionary<string, int> playerMaterials);
    void Upgrade();
}