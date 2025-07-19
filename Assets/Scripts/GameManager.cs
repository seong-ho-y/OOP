using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager Instance { get; set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Dictionary<string, int> playerMaterials = new Dictionary<string, int>();
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
