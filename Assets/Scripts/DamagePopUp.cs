using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 필요
using System.Collections;

public class DamagePopUp : MonoBehaviour
{
    public float moveSpeed = 1f;       // 텍스트가 위로 움직이는 속도
    public float fadeSpeed = 2f;       // 텍스트가 투명해지는 속도 (값이 작을수록 빨리 투명해짐)
    public float destroyTime = 1f;     // 텍스트가 사라지는 총 시간
    public float scaleSpeed = 0.5f;    // 텍스트가 커지는 속도 (선택 사항, 값이 작을수록 빨리 커짐)

    private TextMeshProUGUI tmpText;   // TextMeshProUGUI 컴포넌트 참조
    private Color startColor;          // 텍스트의 초기 색상

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>(); // 컴포넌트 참조 가져오기
        startColor = tmpText.color;                // 초기 색상 저장
    }

    void OnEnable()
    {
        // 오브젝트 풀링을 고려하여 OnEnable에서 초기화
        // 텍스트가 재활용될 때마다 초기 상태로 리셋
        tmpText.color = startColor;
        transform.localScale = Vector3.one; // 초기 스케일 설정 (프리팹 스케일에 따라 조정될 수 있음)
        StartCoroutine(PopUpAnimation()); // 애니메이션 코루틴 시작
    }

    // 외부에서 대미지 값을 받아 텍스트를 설정하는 메서드
    public void SetDamageText(int damageAmount)
    {
        if (tmpText == null) return;
        tmpText.text = damageAmount.ToString(); // 대미지 값 설정

        // 크리티컬 대미지 등 특별한 경우 색상 변경 (예시)
        if (damageAmount > 50) // 예시: 50보다 큰 대미지는 크리티컬로 간주
        {
            tmpText.color = Color.yellow; // 크리티컬 대미지는 노란색
            tmpText.fontSize = 12; // 크리티컬 대미지는 폰트 크기를 더 크게
        }
        else
        {
            tmpText.color = startColor; // 일반 대미지는 초기 색상
            tmpText.fontSize = 10; // 일반 대미지는 기본 폰트 크기
        }
    }

    // 텍스트 애니메이션을 처리하는 코루틴
    private IEnumerator PopUpAnimation()
    {
        float timer = 0f; // 애니메이션 진행 시간
        Vector3 initialPosition = transform.position; // 시작 위치 저장 (월드 좌표)
        Vector3 initialScale = transform.localScale; // 시작 스케일 저장

        while (timer < destroyTime)
        {
            // 위로 이동
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // 서서히 투명해지기
            float alpha = Mathf.Lerp(startColor.a, 0f, timer / fadeSpeed);
            tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, alpha);

            // 서서히 커지기 (선택 사항)
            transform.localScale = Vector3.Lerp(initialScale, initialScale * 1.2f, timer / scaleSpeed); // 시작 스케일에서 1.2배까지 커지게

            timer += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 애니메이션 종료 후 오브젝트 비활성화 (오브젝트 풀링 시) 또는 파괴
        // 일반적으로 오브젝트 풀링을 위해 비활성화하는 것이 효율적입니다.
        gameObject.SetActive(false); 
        // Destroy(gameObject); // 풀링을 사용하지 않는 경우 (비효율적)
    }
}