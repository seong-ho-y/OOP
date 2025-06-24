using UnityEngine;

/// <summary>
/// 플레이어 조작 및 총괄을 담당
/// 진짜 플레이어가 이동하는건 RailFollower에 있지만 여기에서 플레이어 조작을 담당함
/// player의 중앙시스템 (creature, railfollower, weapon 컴포넌트를 가짐)
/// </summary>
public class Player : MonoBehaviour
{
    
    //필드
    //이동속도 관련 변수
    public float normalSpeed;
    
    //dodge 관련 변수
    public float dodgeDuration = 0.39f; // dodge 시간
    public float dodgeCoolDown = 2f; // dodge 쿨타임 - 회피 종료 이후 계산되는거롤 할거임 EndDodge에서 계산
    public float dodgeSpeed = 2f;
    public bool dodgeAble = true;
    public bool isDodging = false;
    
    
    
    private Rigidbody2D _rb;
    private RailFollower _rail;
    private Creature _cr;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rail = GetComponent<RailFollower>();
        _cr = GetComponent<Creature>();
    }
    void Start()
    {
        Debug.Log("Start_Player");
        _rail.moveSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (isDodging)
        {
            Debug.Log("!!저스트 회피!!");
        }
        else
        {
            //_cr.TakeDamage();
        }
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
        if (isDodging || !dodgeAble) return;
        
        isDodging = true;
        dodgeAble = false;
        Debug.Log("Dodge");

        _rail.moveSpeed = dodgeSpeed;
        
        Invoke(nameof(EndDoge), dodgeDuration);
    }

    void EndDoge()
    {
        _rail.moveSpeed = normalSpeed;
        isDodging = false;

        Invoke(nameof(ResetDodge), dodgeCoolDown);
    }

    void ResetDodge()
    {
        dodgeAble = true;
    }
}

