using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Feild Declare
    
    public static event Action OnTap;
    public static event Action OnHoldStarted;
    public static event Action<float> OnHoldReleased; //HoldTime까지 같이 전달해줄거임
    public static event Action<SwipeDirection> OnSwipe;
    
    public enum SwipeDirection{Left, Right, Up, Down}

    // --- 모바일 입력 처리 ---
    private Vector2 touchStartPosition;
    private float touchStartTime;
    public float swipeThreshold = 20f;          // 스와이프 인식을 위한 최소 이동 거리 (픽셀 단위)
    public float tapMaxDuration = 0.2f;         // 탭 인식을 위한 최대 터치 시간 (초)
    public float holdMinDuration = 0.3f;
    
    public bool isHolding = false;

    #endregion
    

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
                isHolding = false;

            }
            // 누르고 있는 상태 감지
            else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // 근데 홀드 로직은 한자리에서만 하고있어야함 (이거는 if로 분기를 한번 더 쳐?)
                if (!isHolding && Time.time - touchStartTime > holdMinDuration)
                {
                    isHolding = true;
                    OnHoldStarted?.Invoke();
                    Debug.Log("홀드 방송");
                }
                
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float touchDuration = Time.time - touchStartTime;
                Vector2 swipeVector = touch.position - touchStartPosition;

                if (isHolding)
                {
                    OnHoldReleased?.Invoke(touchDuration);
                    Debug.Log("릴리즈 방송");
                }
                else if (swipeVector.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(swipeVector.y) > Mathf.Abs(swipeVector.x))
                    {
                        OnSwipe?.Invoke(swipeVector.y>0 ? SwipeDirection.Up : SwipeDirection.Down);
                        Debug.Log("스킬 방송");
                    }
                    else
                    {
                        OnSwipe?.Invoke(swipeVector.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
                        Debug.Log("대쉬 방송");
                    }
                }
                else if (touchDuration <= tapMaxDuration)
                {
                    OnTap?.Invoke();
                    Debug.Log("탭 방송");
                }

                isHolding = false;
            }
        }
    }
}
