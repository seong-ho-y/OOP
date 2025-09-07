using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class InventoryManager : MonoBehaviour
    {
        #region Singleton
        public static InventoryManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        
        #endregion
        
        public Dictionary<BaseItemData, int> PlayerInventory { get; private set; }
        // 플레이어가 가지고 있는 재료 딕셔너리

        private void Start()
        {
            InitializeGameData();
        }
        //초기화 함수
        public void InitializeGameData()
        {
            PlayerInventory = new Dictionary<BaseItemData, int>();
        }
        //재료 추가 함수
        public void AddItemToInventory(BaseItemData itemData, int amount = 1)
        {
            if (itemData == null)
            {
                Debug.LogError("Attempted to add null item to inventory.");
                return;
            }
            if (PlayerInventory.ContainsKey(itemData))
            {
                int currentAmount = PlayerInventory[itemData];
                int newAmount = Mathf.Min(currentAmount + amount, itemData.MaxStack);
                PlayerInventory[itemData] = newAmount;
                Debug.Log($"Added {amount} x {itemData.itemName} to inventory. Current: {PlayerInventory[itemData]}");
            }
            else
            {
                PlayerInventory.Add(itemData, amount);
                Debug.Log($"Added {amount} x {itemData.itemName} to inventory. Current: {PlayerInventory[itemData]}");
            }
        }

        //재료 삭제 함수
        public bool RemoveItemFromInventory(BaseItemData itemData, int amount = 1)
        {
            if (itemData == null)
            {
                Debug.LogError("Attempted to remove null item from inventory.");
                return false;
            }

            if (PlayerInventory.ContainsKey(itemData) && PlayerInventory[itemData] >= amount)
            {
                PlayerInventory[itemData] -= amount;
                if (PlayerInventory[itemData] <= 0)
                {
                    PlayerInventory.Remove(itemData);
                }
                Debug.Log($"Removed {amount} x {itemData.itemName} from inventory. Current: {(PlayerInventory.ContainsKey(itemData) ? PlayerInventory[itemData] : 0)}");
                return true;
            }
            Debug.LogWarning($"Failed to remove {amount} x {itemData.itemName}. Not enough or doesn't exist in inventory.");
            return false;
        }
        public bool HasItem(BaseItemData itemData, int amount = 1)
        {
            return PlayerInventory.ContainsKey(itemData) && PlayerInventory[itemData] >= amount;
        }

        private void HandleStageLoaded(StageData stageData)
        {
            
        }
        private void HandleEnemyDied(Enemy enemy)
        {
            return;
            //
        }
        private void OnEnable()
        {
            GameEvents.OnEnemyDied += HandleEnemyDied;
            GameEvents.OnStageLoaded += HandleStageLoaded;
        }

        private void OnDisable()
        {
            GameEvents.OnEnemyDied -= HandleEnemyDied;
            GameEvents.OnStageLoaded -= HandleStageLoaded;
        }
    }
}
