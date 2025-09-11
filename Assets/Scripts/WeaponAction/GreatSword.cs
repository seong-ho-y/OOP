using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : WeaponAction
{
    private PlayerAttack playerAttackRef; // PlayerAttack 참조 저
    private Player player;
    private RailFollower _rF;
    public GreatSword(PlayerAttack paRef, Player p) // 생성자에서 PlayerAttack 참조를 받음
    {
        playerAttackRef = paRef;
        player = p;
        _rF = paRef.GetComponent<RailFollower>();
    }

    // IWeaponAction 인터페이스의 Attack 메서드 구현
    public IEnumerator Attack(PlayerAttack playerAttack, bool isCharged, float currentChargeTime = 0f, float maxChargeTime = 0f)
    {
        Debug.Log("공격");
        Weapon stats = playerAttack.currentWeaponStats;

        // 1. 공격 애니메이션 재생 시작 (플레이어 이동 애니메이션과 동기화)
        // playerAttack.playerAnimator.SetTrigger("GreatSwordDashAttack");
        //Debug.Log("대검 공격 애니메이션 시작");

        // 2. 공격 이동 시작 (이동하면서 콜라이더 활성화 및 충돌 감지)
        yield return playerAttack.StartCoroutine(playerAttack.PerformAttackMovement(stats.attackMoveSpeed));

        // 3. 데미지 계산 (실제 데미지는 OnTriggerEnter2D에서 적용되지만, 여기서 최종 데미지 값을 설정할 수 있음)
        float finalDamage = stats.baseDamage;
        if (isCharged)
        {
            int chargeLevel = Mathf.Min(Mathf.FloorToInt(currentChargeTime / (maxChargeTime / 5f)), 5);
            finalDamage = stats.baseDamage * (1 + chargeLevel * 0.2f);
        //    Debug.Log($"모아베기 발동! 차징 레벨: {chargeLevel}, 최종 데미지: {finalDamage}");
        }
        else
        {
         //   Debug.Log("대검 기본 공격 발동!");
        }

        // --- 중요: AttackHitboxController가 실제 데미지를 주도록 로직을 분리하는 것이 이상적입니다. ---
        // PlayerAttack의 OnTriggerEnter2D에서 currentWeaponStats.baseDamage 대신 finalDamage를 사용하려면,
        // 이 finalDamage를 PlayerAttack에 임시로 저장하는 방법이 필요합니다.
        // 예를 들어 playerAttack.SetCurrentAttackDamage(finalDamage); 와 같이.
        // 여기서는 일단 간략화하여 baseDamage를 사용합니다.

        // 4. 공격 후 플레이어 복귀
        yield return playerAttack.StartCoroutine(playerAttack.ReturnToRailAfterAttack(stats.attackMoveSpeed));

        //Debug.Log("대검 공격 프로세스 완료");
    }
    
    private Coroutine guardCoroutine;

    public void SwipeDown()
    {
        if (guardCoroutine != null)
        {
            playerAttackRef.StopCoroutine(guardCoroutine);
        }
        guardCoroutine = playerAttackRef.StartCoroutine(GuardRoutine());
        Debug.Log("가드");
    }

    private IEnumerator GuardRoutine()
    {
        //Debug.Log("가드 시작");
        // 차징 초기화
        playerAttackRef.isCharging = false;
        playerAttackRef.currentChargeTime = 0f;
        playerAttackRef.pendingChargeCheck = false;
        _rF.enabled = false;
        playerAttackRef.ShowGuardEffect(1f);

        // 플레이어 이동 멈춤
        playerAttackRef.enabled = false; // 공격 입력/이동 스크립트 비활성화 (필요에 따라)
        player.enabled = false; // 플레이어 컨트롤러 자체 비활성화도 필요할 수 있음

        // 방어력 증가 (임시로 player 스탯에 직접 적용했다고 가정)

        // 방어 효과 유지 시간 1초 대기
        yield return new WaitForSeconds(1.0f);

        // 방어력 원복

        // 플레이어 이동 및 입력 다시 활성화
        playerAttackRef.enabled = true;
        player.enabled = true;
        _rF.enabled = true;

       // Debug.Log("가드 종료");
    }
    public void SwipeUp() { Debug.Log("모으기 강화"); }

    public void SwipeLeft()
    {
        player.Dodge(-1);
       Debug.Log("왼쪽 회피 (대검)");
    }

    public void SwipeRight()
    {
        player.Dodge(1);
        Debug.Log("오른쪽 회피 (대검)");
    }
}