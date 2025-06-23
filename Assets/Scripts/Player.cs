using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어 이동 로직을 담당
    
    //필드
    
    //dodge 관련 변수들
    public float dodgeForce = 1f;
    public float dodgeDuration = 2f;
    public bool isdodging = false;
    
    
    
    private Rigidbody2D _rb;
    private RailFollower _rail;
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rail = GetComponent<RailFollower>();
    }
    void Start()
    {
        Debug.Log("Start_Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }

    void OnJump() //spacebar 입력받기
    {
        Dodge();
    }

    void Dodge() //회피
    //회피는 무기벼롤 다름
    //대검 - 구르기 (짧은 거리, 짧은 무적시간)
    //쌍검 - 스텝 (먼 거리, 긴 무적시간)
    //
    {
        if (isdodging) return;
        
        isdodging = true;
        Debug.Log("Dodge");

        _rail.moveSpeed = 2f;
        
        Invoke(nameof(EndDoge), dodgeDuration);
    }

    void EndDoge()
    {
        _rail.moveSpeed = 1f;
        isdodging = false;
    }
}



public class PlayerInfo : Creature
{
    public PlayerInfo(int health, int stamina, int speed) : base(health, stamina, speed)
    {
        health = 20;
        stamina = 10;
        speed = 5;
    }
}