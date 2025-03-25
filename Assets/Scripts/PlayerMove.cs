using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Player player;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    bool isStep = false;
    float stepTime = 0;
    public static event Action OnStep;
    public static event Action OnTakeDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.Instance.player;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isStep)
        {
            StartCoroutine(Step());
        }
    }

    private void FixedUpdate()
    {
        if (!isStep)
        {
            rb.linearVelocity = moveInput * player.Speed;
        }
    }

    IEnumerator Step() //스텝 코루틴
    {
        isStep = true;
        OnStep?.Invoke();
        float startTime = Time.time;
        while (Time.time < startTime + player.stepTime)
        {
            rb.linearVelocity = Vector2.zero;
            yield return null;
        }

        isStep = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Attack")
        {
            OnTakeDamage?.Invoke();
        }
        Debug.Log("플레이어 충돌");
    }
}
