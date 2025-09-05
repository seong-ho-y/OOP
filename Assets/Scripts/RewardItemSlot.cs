using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class RewardItemSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCount;

    public void SetData(BaseItemData itemData, int amount)
    {
        icon.sprite = itemData.icon;
        itemName.text = itemData.itemName;
        itemCount.text = "X" + amount.ToString();
    }
}
