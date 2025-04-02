using UnityEngine;
using System.Collections.Generic;

public class RailPath : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    public Vector3 GetPoint(int index)
    {
        return points[index].position;
    }

    public int Length => points.Count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
