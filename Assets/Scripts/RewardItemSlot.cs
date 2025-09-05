using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardItemSlot : MonoBehaviour
{
    public Sprite icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;

    public void SetData(BaseItemData itemData, int amount)
    {
        icon = itemData.icon;
        itemName.text = itemData.itemName;
        itemCount.text = "X" + amount.ToString();
    }
}
