using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 이 줄을 꼭 추가해야 합니다.
using System.Collections; // 코루틴(IEnumerator)을 사용하기 위해 이 줄을 추가합니다.

public class DamageTextController : MonoBehaviour
{
    // === Inspector에서 조절할 수 있는 변수들 ===
    public float destroyTime = 1.5f; // 이 시간 후에 텍스트가 사라집니다. (단위: 초)
    public float floatSpeed = 0.5f;  // 텍스트가 위로 떠오르는 속도
    public float fadeSpeed = 2f;     // 텍스트가 서서히 투명해지는 속도 (숫자가 클수록 빨리 투명해짐)

    // === 스크립트 내부에서 사용하는 변수들 ===
    private TextMeshProUGUI textMeshPro; // 실제 텍스트 컴포넌트를 담을 변수
    private Color startColor; // 텍스트의 초기 색상을 저장할 변수

    // 이 스크립트가 처음 활성화될 때 한 번 실행됩니다.
    void Awake()
    {
        // 이 스크립트가 붙어있는 게임 오브젝트에서 TextMeshProUGUI 컴포넌트를 찾아옵니다.
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // 텍스트의 초기 색상을 저장해 둡니다. 나중에 투명도 조절에 사용합니다.
        if (textMeshPro != null)
        {
            startColor = textMeshPro.color;
        }
        else
        {
            Debug.LogError("DamageTextController: TextMeshProUGUI 컴포넌트를 찾을 수 없습니다! 올바른 오브젝트에 스크립트가 붙어있는지 확인하세요.");
        }
    }

    // 외부에서 데미지 값을 전달받아 텍스트를 설정하고 효과를 시작하는 메서드입니다.
    public void SetDamage(int damage)
    {
        if (textMeshPro != null)
        {
            // 전달받은 데미지 값을 텍스트로 설정합니다.
            textMeshPro.text = damage.ToString();
        }

        // 텍스트가 위로 떠오르면서 사라지게 하는 코루틴을 시작합니다.
        StartCoroutine(FadeAndDestroy());
    }

    // 텍스트가 서서히 사라지고 파괴되는 애니메이션을 처리하는 코루틴입니다.
    IEnumerator FadeAndDestroy()
    {
        float timer = 0f; // 경과 시간을 측정할 타이머

        // destroyTime까지 반복합니다.
        while (timer < destroyTime)
        {
            // 텍스트를 위로 조금씩 이동시킵니다.
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

            // 텍스트의 투명도를 조절하여 서서히 사라지게 합니다.
            // Mathf.Lerp는 두 값 사이를 선형적으로 보간해 줍니다.
            float alpha = Mathf.Lerp(startColor.a, 0f, timer / destroyTime * fadeSpeed);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            timer += Time.deltaTime; // 타이머 증가
            yield return null; // 다음 프레임까지 대기 (이게 없으면 한 번에 다 처리됩니다)
        }

        // 설정된 destroyTime이 지나면 이 게임 오브젝트(데미지 텍스트)를 파괴합니다.
        Destroy(gameObject);
    }
}