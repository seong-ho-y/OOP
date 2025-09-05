using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Heal Effect", menuName = "Items/Effects/Heal")]
    public class HealEffect : ItemEffect
    {
        public override void Execute(GameObject target, float effectAmount)
        {
            target.GetComponent<Creature>().Heal(effectAmount);
        }
    }
}