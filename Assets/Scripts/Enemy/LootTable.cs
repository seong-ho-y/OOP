using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

[System.Serializable]
public class LootDropItem
{
    public BaseItemData itemData;
    public int quantity = 1;
    [Range(0f, 100f)] public float dropChance;
}

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot/LootTable")]
public class LootTable : ScriptableObject
{
    public List<LootDropItem> dropList;

    public List<BaseItemData> GetDropItem()
    {
        List<BaseItemData> dropItems = new List<BaseItemData>();
        foreach (var item in dropList)
        {
            for (int i = 0; i < item.quantity; i++)
            {
                float chacne = Random.Range(1, 100);
                if (chacne <= item.dropChance)
                {
                    dropItems.Add(item.itemData);
                }
            }
        }
        return dropItems;
    }
}

