using UnityEngine;
using System.Collections.Generic;

public interface ICraftable
{
    public Dictionary<string, int> GetRequiredMaterials();
    public void OnCrafted();
}
public class BaseItemData : ScriptableObject, ICraftable
{
    [Header("Common Item Information")]
    public string itemName = "New Item";
    public string description = "A generic itme.";
    public Sprite icon;
    public int MaxStack = 999;

    public virtual void UseItem(GameObject player)
    {
        Debug.Log($"Using {itemName}. (BaseItemData default behaviour");
    }
    [Header("Crafting Recipe")]
    public List<MaterialCost> craftingMaterials = new List<MaterialCost>();

    public virtual Dictionary<string, int> GetRequiredMaterials()
    {
        Dictionary<string, int> materialDict = new Dictionary<string, int>();
        foreach (var material in craftingMaterials)
        {
            if (materialDict.ContainsKey(material.itemName))
            {
                materialDict[material.itemName] += material.cost;
            }
            else
            {
                materialDict.Add(material.itemName, material.cost);
            }
        }

        return materialDict;
    }

    public void OnCrafted()
    {
        Debug.Log($"Crafted {itemName}");
    }
}
