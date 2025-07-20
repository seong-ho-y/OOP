using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialItem", menuName = "Items/Material Item Data")]
public class MaterialItemData : BaseItemData
{
    public override void UseItem(GameObject player)
    {
        Debug.Log($"{itemName} is a material and cannot be used directly.");
    }
}
