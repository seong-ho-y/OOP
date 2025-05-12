using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    
}

public interface IEquiable
{
    string Name { get; }
    public void Equip();
    public void UnEquip();
}

public interface ICraftable
{
    bool CanCraft(Dictionary<string, int> playerMaterials);
    void Craft();
}

public interface IUpgradable
{
    bool CanUpgrade(Dictionary<string, int> playerMaterials);
    void Upgrade();
}


