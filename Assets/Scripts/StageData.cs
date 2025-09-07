using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

[CreateAssetMenu(fileName = "New Stage Data", menuName = "Stage/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public string stageName;
    public int stageNumber;
    public GameObject stageRail;

    [Header("클리어 조건")]
    public List<GameObject> enemyPrefabsToSpawn;
    public float timeLimit = 300f;

    [Header("보상 정보")]
    public LootTable rewardLootTable;

}