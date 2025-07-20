using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }
    // 플레이어가 가지고 있는 재료 딕셔너리
    public Dictionary<string, int> PlayerMaterials { get; private set; } //외부에서 get만 허용
    public Dictionary<BaseItemData, int> PlayerItems { get; private set; } 
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGameData(); //게임 시작 시 데이터 초기화 함수 호출
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //초기화 함수
    private void InitializeGameData()
    {
        PlayerMaterials = new Dictionary<string, int>();
        PlayerItems = new Dictionary<BaseItemData, int>();
    }
    
    //재료 추가 함수
    public void AddMaterial(string materialName, int amount)
    {
        if (PlayerMaterials.ContainsKey(materialName))
        {
            PlayerMaterials[materialName] += amount;
        }
        else
        {
            PlayerMaterials.Add(materialName, amount);
        }
        Debug.Log($"Added {amount} {materialName}. Current {materialName}: {PlayerMaterials[materialName]}");
    }
    //재료 삭제 함수
    public void UseMaterial(string materialName, int amount)
    {
        if (PlayerMaterials.ContainsKey(materialName) && PlayerMaterials[materialName] >= amount)
        {
            PlayerMaterials[materialName] -= amount;
            Debug.Log($"Removed {amount} {materialName}. Current {materialName}: {PlayerMaterials[materialName]}");
        }
        else
        {
            Debug.Log("Did not find material or Not enough");
        }
    }

    public void AddItemToInventory(BaseItemData itemData, int amount = 1)
    {
        
    }
    public delegate void PlusDelegate();
    void StageClear()
    {
        
    }
    void NextStage()
    {

    }
    void XpUp(int level)
    {
    }
}
