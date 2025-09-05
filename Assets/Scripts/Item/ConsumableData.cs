using Item;
using Manager;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Data", menuName = "Items/Consumable Item Data")]
public class ConsumableData : BaseItemData
{
    [Header("Consumable Specifics")]
    public ItemEffect ItemEffect;
    public float EffectAmount = 0f;
    public float Cooldown = 0;

    public override void UseItem(GameObject effectTarget)
    {
        Debug.Log($"Using {itemName}.");
        if (ItemEffect != null)
        {
            ItemEffect.Execute(effectTarget, EffectAmount);
        }
        InventoryManager.Instance.RemoveItemFromInventory(this, 1);
    }
}

