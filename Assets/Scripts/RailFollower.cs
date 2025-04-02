using UnityEngine;

public class RailFollower : MonoBehaviour
{
    public RailPath path;

    public float moveSpeed = 2f;

    private int currentIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private bool reverse = false;

    void Update()
    {
        if (!path || path.Length == 0) return;

        Vector3 targetPos = path.GetPoint(currentIndex);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
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
}
