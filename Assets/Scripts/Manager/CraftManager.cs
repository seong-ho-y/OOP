using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool CanCraft(ICraftable item)
    {
        
        Dictionary<string, int> playerMaterials = GameManager.Instance.PlayerMaterials;
        if (playerMaterials == null || item.GetRequiredMaterials() == null)
        {
            throw new System.ArgumentNullException();
        }
        if (playerMaterials.Count == 0)
        {
            Debug.Log("Not enough player materials");
            return false;
        }

        foreach (KeyValuePair<string, int> requiredMaterial in item.GetRequiredMaterials())
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
    // 장비 제작하는 메서드
    public void Craft(ICraftable craftableItem) // 매개변수 이름을 item -> craftableItem으로 변경 (더 명확하게)
    {
        // GameManager.Instance.PlayerMaterials는 이미 CanCraftItem에서 접근하므로 여기서 다시 가져올 필요 없음
        // if (CanCraft(craftableItem)) // CanCraft 메서드를 호출하여 제작 가능 여부 재확인
        // CanCraftItem에서 이미 로그를 찍어주므로, 여기서 별도 로그는 생략 가능

        // craftableItem이 BaseItemData인지 확인하고 캐스팅합니다.
        // ItemData는 ICraftable을 구현하므로 이 캐스팅은 항상 성공합니다.
        if (craftableItem is BaseItemData itemData)
        {
            // 먼저 제작 가능한지 다시 확인합니다.
            // Craft 메서드 내에서 CanCraftItem을 다시 호출하여 안정성을 높입니다.
            if (CanCraft(itemData))
            {
                Dictionary<string, int> requiredMaterials = itemData.GetRequiredMaterials();

                // 1. 재료 소모
                foreach (KeyValuePair<string, int> requiredMat in requiredMaterials)
                {
                    string materialName = requiredMat.Key;
                    int requiredAmount = requiredMat.Value;

                    GameManager.Instance.UseMaterial(materialName, requiredAmount);
                }

                // 2. 인벤토리에 아이템 추가
                // itemData는 BaseItemData 타입이므로, AddItemToInventory 메서드에 바로 전달할 수 있습니다.
                GameManager.Instance.AddItemToInventory(itemData);

                // 3. 아이템의 제작 완료 콜백 호출
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
            // 이 경우는 발생해서는 안 되는 상황 (ICraftable이 BaseItemData가 아닐 때)
            Debug.LogError($"Crafting failed: Provided ICraftable is not a BaseItemData type. Type: {craftableItem.GetType().Name}");
        }
    }
}
