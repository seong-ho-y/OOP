using System.ComponentModel.Design;
using System.IO;
using UnityEngine;

public class RailFollower : MonoBehaviour
{

    private Vector2 _lastPosition;
    public Vector2 CurrentDirection { get; private set; } //플레이어가 현재 이동하고 있는 방향을 저장 (이동기에 쓰임)
    public RailPath path;
    
    public float moveSpeed;

    public int currentIndex = 0;
    
    public bool reverse = false;

    void Update()
    {
        if (!path || path.Length == 0) return;
        
        Vector3 targetPos = path.GetPoint(currentIndex);

        if (!reverse) //CurrentDirection 계산
        {
            if (currentIndex > 0)
                CurrentDirection = (path.GetPoint(currentIndex) - path.GetPoint(currentIndex - 1)).normalized;
        }
        else
        {
            if (currentIndex < path.Length - 1)
                CurrentDirection = (path.GetPoint(currentIndex) - path.GetPoint(currentIndex + 1)).normalized;
        }
        //이동하기
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        
        //왕복시켜주는 로직
        if (Vector2.Distance(transform.position, targetPos) < 0.05f) //position이랑 가까워지면 인덱스 갱신
        {
            if (!reverse) currentIndex++;
            else currentIndex--;

            if (currentIndex >= path.Length)
            {
                currentIndex = path.Length - 2;
                reverse = true;
            }
            else if (currentIndex < 0)
            {
                currentIndex = 1;
                reverse = false;
            }
        }
    }
    public void NudgeIndex(int direction)
    {
        if (!path || path.Length < 2) return;

        // 예외처리
        if (direction < 0 && currentIndex <= 0) return; // 왼쪽으로 못 감
        if (direction > 0 && currentIndex >= path.Length - 1) return; // 오른쪽으로 못 감

        
        if (!((direction < 0 && reverse) || (direction > 0 && !reverse)))
        {
            currentIndex += direction;
            if (direction < 0)
            {
                reverse = true;
            }
            else
            {
                reverse = false;
            }
        }

    }
}
