using UnityEngine;

namespace Item
{
    public abstract class ItemEffect : ScriptableObject
    {
        public abstract void Execute(GameObject target, float effectAmount);
    }
}

