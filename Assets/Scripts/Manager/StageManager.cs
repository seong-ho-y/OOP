using System;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public enum StageState {Ready, InProgress, Cleared, Failed}

    public class StageManager : MonoBehaviour
    {
        #region Singleton
        public static StageManager Instance { get; private set; }

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
            
            //필드 초기화
            enemyRewardList = new List<BaseItemData>();
        }
        #endregion

        [Header("스테이지 상태")]
        public StageData currentStageData;
        public StageState CurrentStageState { get; private set; }

        [Header("적 구성")]
        public int totalEnemyCnt;
        public int defeatedEnemyCnt;
    
        // 적 보상 목록을 저장할 리스트
        private List<BaseItemData> enemyRewardList;
        private float stageTimer;
        
        private void HandleStageLoaded(StageData stageData)
        {
            currentStageData = stageData;
            CurrentStageState = StageState.InProgress;
            
            totalEnemyCnt = currentStageData.enemyPrefabsToSpawn.Count;
            defeatedEnemyCnt = 0;
            enemyRewardList.Clear();
            
            stageTimer = Time.time;
            Debug.Log($"{currentStageData.stageName} Loaded. Quest Start");
        }
        
        private void HandleEnemyDied(Enemy enemy)
        {
            defeatedEnemyCnt++;
            LootTable eLT = enemy.lootTable;
            if (eLT != null)
            {
                enemyRewardList.AddRange(eLT.GetDropItem());
            }
            if(CheckClear()) GameEvents.ReportStageClear(enemyRewardList, stageTimer,"!!Quest Completed!!");
        }

        private bool CheckClear()
        {
            return (defeatedEnemyCnt >= totalEnemyCnt);
        }
        
        private void StageClear(List<BaseItemData> reward, float clearTime, string header)
        {
            throw new NotImplementedException();
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