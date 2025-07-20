using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public static CraftManager Instance {get; private set;}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public bool CanCraft(ICraftable craftableItem)
    {
        string itemName = "Unknown";
        if (craftableItem is BaseItemData baseItemData)
        {
            itemName = baseItemData.itemName;
        }

        // 필요한 재료를 Dictionary<BaseItemData, int> 타입으로 가져옵니다.
        Dictionary<BaseItemData, int> requiredMaterials = craftableItem.GetRequiredMaterials();

        if (requiredMaterials == null || requiredMaterials.Count == 0)
        {
            Debug.Log($"No materials required for {itemName}. Crafting is possible.");
            return true;
        }

        foreach (var requiredMat in requiredMaterials)
        {
            BaseItemData materialData = requiredMat.Key;
            int requiredAmount = requiredMat.Value;

            if (materialData == null)
            {
                Debug.LogError($"Crafting recipe for {itemName} contains a null material reference!");
                return false;
            }

            // GameManager.Instance.HasItem 메서드를 사용하여 재료 보유 여부 확인
            if (!GameManager.Instance.HasItem(materialData, requiredAmount))
            {
                Debug.Log($"Cannot craft {itemName}: Not enough {materialData.itemName}. Needed: {requiredAmount}, Have: {(GameManager.Instance.PlayerInventory.ContainsKey(materialData) ? GameManager.Instance.PlayerInventory[materialData] : 0)}");
                return false;
            }
        }

        Debug.Log($"Able to craft {itemName}.");
        return true;
    }
    // 장비 제작하는 메서드
    public void Craft(ICraftable craftableItem)
    {
        if (craftableItem is BaseItemData itemData)
        {
            if (CanCraft(itemData))
            {
                // 재료 소모
                Dictionary<BaseItemData, int> requiredMaterials = itemData.GetRequiredMaterials();
                foreach (var requiredMat in requiredMaterials)
                {
                    GameManager.Instance.RemoveItemFromInventory(requiredMat.Key, requiredMat.Value); // 이제 RemoveItemFromInventory 사용
                }

                // 인벤토리에 제작된 아이템 추가
                GameManager.Instance.AddItemToInventory(itemData);

                itemData.OnCrafted();

                Debug.Log($"Successfully crafted {itemData.itemName} and added to inventory!");
            }
            else
            {
                Debug.LogWarning($"Crafting failed for {itemData.itemName}. Materials not sufficient.");
            }
        }
        else
        {
            Debug.LogError($"Crafting failed: Provided ICraftable is not a BaseItemData type. Type: {craftableItem.GetType().Name}");
        }
    }
}
