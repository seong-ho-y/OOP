using UnityEngine;

public class ConsumableData : BaseItemData
{
    [Header("Consumable Specifics")]
    public float EffectAmount = 0f;
    public int Cooldown = 0;

    public override void UseItem(GameObject player)
    {
        Debug.Log($"Using {itemName}.");
        //GameManager.Instance.RemoveItem
    }
}
