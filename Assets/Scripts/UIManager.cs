using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI dodgeText;

    private void OnEnable()
    {
        PlayerMove.OnStep += ShowDodgeText; //이벤트 구독
        PlayerMove.OnTakeDamage += ShowDamageText;
    }

    private void OnDisable()
    {
        PlayerMove.OnStep -= ShowDodgeText; //이벤트 해제
    }

    private void ShowDodgeText()
    {
        dodgeText.text = "회피 중!";
        Invoke("ClearText", 0.5f); // 0.5초 후 텍스트 제거
    }
    void ShowDamageText()
    {

    }

    private void ClearText()
    {
        dodgeText.text = "";
    }
}
