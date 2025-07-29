using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    // --- 공통 공격 설정 ---
    public IWeaponAction currentWeaponAction; // 현재 장착된 무기 액션 인터페이스
    public WeaponStats currentWeaponStats;   // 현재 장착된 무기의 스탯 (선택 사항: 데미지 계산 등에 활용)
    

    public Animator playerAnimator; // 플레이어 애니메이터 (옵션)
    public LayerMask enemyLayer;    // 적으로 인식할 레이어 (Inspector에서 설정)
    
    // --- 플레이어 이동 관련 ---
    // 플레이어의 레일 이동을 제어하는 스크립트 (예: RailFollow.cs)
    private RailFollower _railFollowComponent; // 실제 RailFollow 스크립트 타입으로 변경 권장
    private Vector3 initialPlayerPosition; // 공격 시작 전 플레이어 위치
    private bool isAttackingMovement = false; // 공격 이동 중인지 여부
    
    // --- 이번 공격으로 몬스터를 타격했는지 여부 ---
    private bool monsterHitThisAttack = false; 
    
    [Header("Attack Movement Target")]
    public float attackTargetYPosition = 10f; // 플레이어가 공격 시 도달할 목표 Y 좌표 (월드 좌표계)

    // --- 차징 공격을 위한 변수 (대검/해머 등) ---
    // 이 변수는 장착된 무기 타입에 따라 true/false가 되어야 합니다.
    // GreatSword 같은 클래스에서 직접 이 정보를 제공하는 것이 더 좋습니다.
    // 여기서는 일단 public으로 두어 테스트 편의성을 높입니다.
    public bool weaponSupportsChargeAttack = false; // 현재 장착된 무기가 차징 공격을 지원하는가?
    public float maxChargeTime = 2.0f;           // 최대 차징 시간
    private float currentChargeTime;
    private bool isCharging = false;             // 현재 차징 중인가?
    private Coroutine currentAttackCoroutine; // 현재 진행 중인 공격 코루틴 참조

    // --- 모바일 입력 처리 ---
    private Vector2 touchStartPosition;
    private float touchStartTime;
    public float swipeThreshold = 50f;          // 스와이프 인식을 위한 최소 이동 거리 (픽셀 단위)
    public float tapMaxDuration = 0.2f;         // 탭 인식을 위한 최대 터치 시간 (초)
    
    // --- 공격 콜라이더 ---
    [Header("Attack Collision")]
    public Collider2D attackHitbox; // 플레이어의 공격 판정용 콜라이더 (Inspector에서 할당)
    private HashSet<Creature> hitEnemiesInCurrentAttack; // 현재 공격에서 이미 타격한 적들


    // 스크립트 시작 시 기본 무기 장착 (테스트용)
    void Start()
    {
        _railFollowComponent = GetComponent<RailFollower>();
        // 실제 게임에서는 무기 교체 시스템을 통해 호출될 것입니다.
        EquipWeapon(new GreatSword(this), new WeaponStats(100f, ElementType.None, DamageType.Slash, 1.0f, 8f, 0.2f)); // 초기 무기 장착
        if (attackHitbox != null)
        {
            attackHitbox.enabled = false;
        }
        else
        {
            Debug.Log("Debug.LogError(\"Attack Hitbox Collider2D not assigned on PlayerAttack script!");
        }
        hitEnemiesInCurrentAttack = new HashSet<Creature>(); // HashSet 초기화
    }

    void Update()
    {
        HandleTouchInput(); // 매 프레임 터치 입력 처리
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                touchStartTime = Time.time;
                isCharging = false;

                if (!isAttackingMovement) // 공격 중이 아닐 때만 시작
                {
                    if (weaponSupportsChargeAttack)
                    {
                        isCharging = true;
                        currentChargeTime = 0f;
                        // playerAnimator.SetTrigger("ChargeStart");
                    }
                }
            }
            else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (isCharging)
                {
                    currentChargeTime += Time.deltaTime;
                    _railFollowComponent.enabled = false;
                    // playerAnimator.SetFloat("ChargeProgress", currentChargeTime / maxChargeTime);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float touchDuration = Time.time - touchStartTime;
                Vector2 touchEndPosition = touch.position;
                Vector2 swipeVector = touchEndPosition - touchStartPosition;
                float swipeMagnitude = swipeVector.magnitude;

                // 공격 이동 중에는 다른 입력 무시 (방어/회피 등은 공격 중에도 허용할지 결정)
                if (isAttackingMovement)
                {
                    // 여기서는 일단 공격 중 다른 입력은 무시하도록 합니다.
                    // 만약 공격 중에도 회피/가드 등이 가능하다면, 이 부분을 조건부로 처리
                    // isCharging = false; // 공격 중에는 차징 해제
                    return;
                }

                // 스와이프 처리
                if (swipeMagnitude > swipeThreshold)
                {
                    if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x))
                    {
                        if (swipeVector.y > 0) currentWeaponAction.SwipeUp();
                        else currentWeaponAction.SwipeDown();
                    }
                    else
                    {
                        if (swipeVector.x > 0) currentWeaponAction.SwipeRight();
                        else currentWeaponAction.SwipeLeft();
                    }
                }
                // 탭 또는 홀드 & 릴리즈 공격 처리
                else
                {
                        // 무기 액션의 Attack 코루틴 시작
                        // currentAttackCoroutine = StartCoroutine(currentWeaponAction.Attack(this, isCharging, currentChargeTime, maxChargeTime));
                        // isCharging 상태에 따라 isCharged 인수를 전달
                        if (touchDuration <= tapMaxDuration)
                        {
                            // 탭
                            currentAttackCoroutine = StartCoroutine(currentWeaponAction.Attack(this, false)); // 차징 아님
                        }
                        else
                        {
                            // 홀드 & 릴리즈
                            currentAttackCoroutine =
                                StartCoroutine(currentWeaponAction.Attack(this, true, currentChargeTime,
                                    maxChargeTime)); // 차징 맞음
                        }
                }
                isCharging = false; // 터치 종료 시 차징 상태 해제
            }
        }
    }

    // 무기 장착 함수 (외부에서 호출될 수 있도록 public)
    public void EquipWeapon(IWeaponAction newWeaponAction, WeaponStats newWeaponStats)
    {
        currentWeaponAction = newWeaponAction;
        currentWeaponStats = newWeaponStats;

        // 장착된 무기에 따라 차징 지원 여부 설정 (예시)
        weaponSupportsChargeAttack = (newWeaponAction is GreatSword); // 대검이라면 차징 지원

        Debug.Log($"무기 장착: {newWeaponAction.GetType().Name}, 기본 데미지: {newWeaponStats.baseDamage}");

        // 여기에서 애니메이터 컨트롤러 변경, 모델 교체 등 시각적인 무기 교체 로직 추가
    }
    
    // --- 히트 감지 공통 함수 ---
    void OnTriggerEnter2D(Collider2D other)
    {
        // 공격 히트박스가 활성화된 경우에만 반응하며, 이미 몬스터를 맞췄다면 추가적인 처리 방지
        if (!attackHitbox.enabled || !isAttackingMovement || monsterHitThisAttack) return; 

        // 몬스터 레이어에 속하는지 확인
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Creature targetCreature = other.GetComponent<Creature>();
            if (targetCreature != null)
            {
                // 이미 이번 공격에서 타격한 적이 아니라면
                if (!hitEnemiesInCurrentAttack.Contains(targetCreature))
                {
                    hitEnemiesInCurrentAttack.Add(targetCreature); // 타격한 적 목록에 추가

                    // 데미지 계산 및 적용
                    Debug.Log($"Hit: {targetCreature.CreatureName} during attack movement! Applying base damage: {currentWeaponStats.baseDamage}");
                    targetCreature.TakeDamage((int)currentWeaponStats.baseDamage); 

                    // --- 핵심 변경: 몬스터 피격 성공 시 플래그 설정 ---
                    monsterHitThisAttack = true; 
                    Debug.Log("몬스터 피격 성공! 공격 이동 조기 중단 신호 보냄.");

                    // (옵션) 몬스터 피격 시 즉시 공격 히트박스 비활성화 (추가적인 몬스터 타격 방지)
                    // attackHitbox.enabled = false; 
                }
            }
        }
    }


    // --- 공격 이동 제어 메서드 (2D 버전으로 수정) ---
    public IEnumerator PerformAttackMovement(float moveDistance, float moveDuration)
    {
        if (_railFollowComponent != null)
        {
            _railFollowComponent.enabled = false; // 레일 이동 스크립트 비활성화
            Debug.Log("공격 시작: 레일 멈춤");
        }
        
        initialPlayerPosition = transform.position; // 공격 시작 위치 저장

        // 목표 위치는 initialPlayerPosition의 X, Z는 그대로 유지하고 Y만 attackTargetYPosition으로 변경
        Vector3 targetPosition = new Vector3(initialPlayerPosition.x, attackTargetYPosition, initialPlayerPosition.z); 
        
        float startTime = Time.time;
        float endTime = startTime + moveDuration;
        bool reachedEndNormally = true; // 끝까지 도달했는지 여부 플래그

        // 공격 히트박스 활성화 및 기록 초기화
        if (attackHitbox != null)
        {
            attackHitbox.enabled = true;
            hitEnemiesInCurrentAttack.Clear(); 
            Debug.Log("공격 히트박스 활성화");
        }

        // --- 이동 루프: 시간이 끝나거나 몬스터 피격 시까지 ---
        while (Time.time < endTime) 
        {
            if (monsterHitThisAttack) // 몬스터가 피격되어 플래그가 설정되면
            {
                Debug.Log("몬스터 피격으로 인해 공격 이동 조기 중단.");
                reachedEndNormally = false; // 끝까지 도달하지 못했음을 표시
                break; // 이동 루프 즉시 종료
            }

            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(initialPlayerPosition, targetPosition, t);
            yield return null; 
        }

        // --- 이동 종료 처리 ---
        // 이동이 끝났을 때 (시간이 다 되거나 몬스터를 맞춰서 중단)
        if (reachedEndNormally) // 끝까지 도달한 경우에만 최종 위치 보정
        {
            transform.position = targetPosition; 
            Debug.Log("공격 이동 완료 (목표 Y: " + attackTargetYPosition + "까지 도달).");
        }
        else // 몬스터 피격으로 조기 종료된 경우, 현재 위치에서 복귀 시작
        {
            Debug.Log("몬스터 피격으로 현재 위치에서 복귀 시작.");
            // 위치는 이미 몬스터 피격 시의 위치
        }
        
        // 공격 히트박스 비활성화 (항상)
        if (attackHitbox != null)
        {
            attackHitbox.enabled = false;
            Debug.Log("공격 히트박스 비활성화.");
        }

        // --- 복귀 로직 시작 ---
        // 몬스터 피격 여부와 상관없이 이 시점에서 항상 복귀 코루틴 호출
        yield return StartCoroutine(ReturnToRailAfterAttack(currentWeaponStats.attackMoveDuration));

        // 모든 공격 프로세스가 완료된 후 최종 상태 업데이트
        isAttackingMovement = false; // 공격 상태 종료 (재공격 가능)
        Debug.Log("모든 공격 프로세스 완료.");
    }

    public IEnumerator ReturnToRailAfterAttack(float returnDuration)
    {
        Vector3 currentAttackEndPosition = transform.position; // 현재 멈춘 위치에서 복귀 시작
        float startTime = Time.time;
        float endTime = startTime + returnDuration;

        Debug.Log("레일로 복귀 시작.");
        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / returnDuration;
            transform.position = Vector3.Lerp(currentAttackEndPosition, initialPlayerPosition, t);
            yield return null;
        }
        transform.position = initialPlayerPosition; // 정확한 원위치로 복귀

        if (_railFollowComponent != null)
        {
            _railFollowComponent.enabled = true; // 레일 이동 스크립트 재활성화
            Debug.Log("레일 이동 재개.");
        }
    }

    // --- 공격 범위 디버깅 ---
    void OnDrawGizmosSelected()
    {
        if (currentWeaponStats != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentWeaponStats.attackRange);

            Gizmos.color = Color.blue;
            // 공격 이동 목표 지점 시각화 (인스펙터의 attackTargetYPosition 사용)
            Vector3 targetGizmoPos = new Vector3(transform.position.x, attackTargetYPosition, transform.position.z);
            Gizmos.DrawWireSphere(targetGizmoPos, 0.2f);
            Gizmos.DrawLine(transform.position, targetGizmoPos);
        }
    }
}
