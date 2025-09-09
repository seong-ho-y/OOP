using UnityEngine;

namespace Manager
{
    public class StatManager : MonoBehaviour
    {
        private Player player;
        private EquipmentManager equipmentManager;
        
        public float Attack { get; private set; }
        public float Defense { get; private set; }
        public float Speed { get; private set; }
        public float MaxHealth { get; private set; }
        public float HealthRegen { get; private set; }
        public float AttackSpeed {get; private set;}
        public float MaxStamina { get; private set; }
        public float StaminaUsage { get; private set; }
        public float StaminaRegen { get; private set; }
        public float DodgeTime { get; private set; }
        public float DodgeSpeed { get; private set; }
        public float FireDefense { get; private set; }
        public float IceDefense { get; private set; }
        public float WaterDefense { get; private set; }
        public float ElectricDefense { get; private set; }
        public float PoisonDefense { get; private set; }
        public float ParalysisDefense { get; private set; }
        

        void Awake()
        {
            player = GetComponent<Player>();
            equipmentManager = GetComponent<EquipmentManager>();
        }

        void UpdateStats()
        {
            Attack = player.BaseDamage;
            Defense = player.Defense;
            Speed = player.MoveSpeed;
            MaxHealth = player.MaxHealth;
            HealthRegen = 0;
            AttackSpeed = 0;
            MaxStamina = 0;
            StaminaUsage = 0;
            StaminaRegen = 0;
            DodgeTime = 0;
            DodgeSpeed = 0;
            FireDefense = 0;
            IceDefense = 0;
            WaterDefense = 0;
            ElectricDefense = 0;
            PoisonDefense = 0;
            ParalysisDefense = 0;
        }
        
    }
}