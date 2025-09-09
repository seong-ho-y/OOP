using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class RewardManager : MonoBehaviour
    {
        #region Singleton
        public static RewardManager Instance { get; private set; }

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
        
        public GameObject rewardPanel;
        public TextMeshProUGUI headerText;
        public Transform itemListParent;
        public GameObject rewardItemSlotPrefab;
        public TextMeshProUGUI clearTimeText;
        

        private void ShowRewardPanel(List<BaseItemData> rewards, float clearTime, string header)
        {
            Debug.Log("ShowRewardPanel Called");
            // 1. 이전 슬롯 초기화
            foreach (Transform child in itemListParent)
            {
                Destroy(child.gameObject);
            }
            
            // 2. rewards 탐색
            var itemCounts =  new Dictionary<BaseItemData, int>();
            foreach (var item in rewards)
            {
                if (itemCounts.ContainsKey(item))
                {
                    itemCounts[item] += 1;
                }
                else
                {
                    itemCounts[item] = 1;
                }
            }
            // 3-1. 헤더출력
            headerText.text = header;
            
            // 3. 클리어 시간
            if (clearTime >= 0)
            {
                clearTimeText.gameObject.SetActive(true);
                int minute = (int)clearTime / 60;
                float second = clearTime % 60;
                clearTimeText.text = $"{minute:00}:{second:00.00}"; // :과 조합을 통해 형식 지정도 가능
            }
            else
            {
                clearTimeText.gameObject.SetActive(false);
            }
            // 4. UI 슬롯 생성
            foreach (var slot in itemCounts)
            {
                GameObject itemSlot = Instantiate(rewardItemSlotPrefab, itemListParent);
                itemSlot.GetComponent<RewardItemSlot>().SetData(slot.Key, slot.Value);
            }
            
            // 5. 활성화
            rewardPanel.SetActive(true);
        }


        private void OnEnable()
        {
            GameEvents.OnStageClear += ShowRewardPanel;
        }


        private void OnDisable()
        {
            GameEvents.OnStageClear -= ShowRewardPanel;
        }
    }
}