using System.Collections.Generic;
using UnityEngine;

public class BaseEquipment : MonoBehaviour
{

}

public interface IEquiable
{
    string Name { get; }
    public void Equip();
    public void UnEquip();
}

public interface IUpgradable
{
    bool CanUpgrade(Dictionary<string, int> playerMaterials);
    void Upgrade();
}


