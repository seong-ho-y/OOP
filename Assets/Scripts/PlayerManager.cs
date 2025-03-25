using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance {  get; private set; }
    public Player player;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            player = new Player(20, 10, 3);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Player : Creature
{
    public Player(int health, int stamina, int speed) : base(health, stamina, speed) //생성자
    {

    }
    public float Xp { get; private set; } = 0f;
    public float stepTime { get; private set; } = 0.3f;

    public void XpUp(int level)
    {

        if (level > 5)
        {
            Xp += 1f;
        }
        else
        {
            Xp += 0.5f;
        }
        Debug.Log($"플레이어의 현재 Xp : {Xp}");
    }

}
