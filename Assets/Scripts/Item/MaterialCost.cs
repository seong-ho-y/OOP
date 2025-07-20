using UnityEngine;
using System;

// MaterialCost는 장비(무기, 방어구, 마력석) 생산 및 강화에 필요한 재료들을 깔끔하게 관리하기 위한 클래스
[Serializable]
public class MaterialCost
{
    public string itemName;
    public int cost;
}
