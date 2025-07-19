using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour, IEquiable, ICraftable, IUpgradable
{
    public string Name { get; }
    public void Equip()
    {
        throw new System.NotImplementedException();
    }

    public void UnEquip()
    {
        throw new System.NotImplementedException();
    }

    //장비를 해금시키는 메서드 (장비 제작에 필요한 소재를 다 모았을 때에 장비UI, 제작, 강화, 뽑기 해금
    public bool CanCraft(Dictionary<string, int> playerMaterials, Dictionary<string, int> equipmentMaterials)
    {
        if (playerMaterials == null || equipmentMaterials == null)
        {
            throw new System.ArgumentNullException();
        }
        if (playerMaterials.Count == 0)
        {
            Debug.Log("Not enough player materials");
            return false;
        }

        foreach (KeyValuePair<string, int> requiredMaterial in equipmentMaterials)
        {
            string materialName = requiredMaterial.Key;
            int requiredAmount = requiredMaterial.Value;
            //재료 가지고 있는지 비교
            if (!playerMaterials.ContainsKey(materialName))
            {
                Debug.Log("Not enough player materials");
                return false;
            }
            //요구 개수랑 비교하기
            int playerMateiralAmount = playerMaterials[materialName];
            if (playerMateiralAmount < requiredAmount)
            {
                Debug.Log("Not enough player materials");
                return false;
            }
        }
        //제작 가능할 때
        Debug.Log("Able to craft equipment");
        return true;
    }

    public void Craft(Dictionary<string, int> playerMaterials, Dictionary<string, int> equipmentMaterials)
    {
        if (CanCraft(playerMaterials, equipmentMaterials))
        {
            foreach (KeyValuePair<string, int> requiredMaterial in equipmentMaterials)
            {
                string materialName = requiredMaterial.Key;
                int requiredAmount = requiredMaterial.Value;


                int playerMateiralAmount = playerMaterials[materialName];
                playerMateiralAmount -= requiredAmount;
            }
        }
    }

    public bool CanUpgrade(Dictionary<string, int> playerMaterials)
    {
        throw new System.NotImplementedException();
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}

public interface IEquiable
{
    string Name { get; }
    public void Equip();
    public void UnEquip();
}

public interface ICraftable
{
    bool CanCraft(Dictionary<string, int> playerMaterials, Dictionary<string, int> equipmentMaterials);
    void Craft(Dictionary<string, int> playerMaterials, Dictionary<string, int> equipmentMaterials);
}

public interface IUpgradable
{
    bool CanUpgrade(Dictionary<string, int> playerMaterials);
    void Upgrade();
}


