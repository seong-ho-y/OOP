using UnityEngine;
using System.Collections.Generic;

public interface ICraftable
{
    public Dictionary<BaseItemData, int> GetRequiredMaterials();
    public void OnCrafted();
}
public class BaseItemData : ScriptableObject, ICraftable
{
    [Header("Common Item Information")]
    public string itemName = "New Item";
    public string description = "A generic item.";
    public Sprite icon;
    public int MaxStack = 999;

    public virtual void UseItem(GameObject player)
    {
        Debug.Log($"Using {itemName}. (BaseItemData default behaviour");
    }
    [Header("Crafting Recipe")]
    public List<MaterialCost> craftingMaterials = new List<MaterialCost>();

    public virtual Dictionary<BaseItemData, int> GetRequiredMaterials()
    {
        Dictionary<BaseItemData, int> materialsDict = new Dictionary<BaseItemData, int>();
        foreach (var matCost in craftingMaterials)
        {
            if (matCost.MaterialItemData == null)
            {
                Debug.LogError($"MaterialCost in {itemName} has a null MaterialItemData reference!");
                continue;
            }

            if (materialsDict.ContainsKey(matCost.MaterialItemData))
            {
                materialsDict[matCost.MaterialItemData] += matCost.cost;
            }
            else
            {
                materialsDict.Add(matCost.MaterialItemData, matCost.cost);
            }
        }
        return materialsDict;
    }

    public void OnCrafted()
    {
        Debug.Log($"Crafted {itemName}");
    }
}
