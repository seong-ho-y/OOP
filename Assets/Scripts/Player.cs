using Unity.VisualScripting;using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name); 
        
    }
}



public class PlayerInfo : Creature
{
    public PlayerInfo(int health, int stamina, int speed) : base(health, stamina, speed)
    {
        health = 20;
        stamina = 10;
        speed = 5;
    }
}