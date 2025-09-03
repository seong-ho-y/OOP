using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        // 싱글톤 인스턴스
        public static GameManager Instance { get; private set; }
  
    
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InventoryManager.Instance.InitializeGameData();
            }
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
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
}
