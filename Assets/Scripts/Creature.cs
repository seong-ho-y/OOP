using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

//Creature 클래스가 가져야할 기본적인 것들
//TakeDamage, Heal, UseStamina, 
public class Creature
{
    private int _health;
    public int Health
    {
        get { return _health; }
        private set
        {
            if (value < 0) _health = 0; //유효성검사를 통해 음수를 방지할 수 있다
            else _health = value;
        }
    }
    public string Name { get; private set; }
    public int Stamina { get; private set; }
    public int Damage { get; private set; }
    public int Speed { get; private set; }
    public Creature(int health, int stamina, int speed)
    {
        this.Health = health;
        this.Stamina = stamina;
        this.Speed = speed;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{damage}만큼의 피해를 입었습니다. 남은 체력 : {Health}");
    }
    public void Heal(int power)
    {
        Health += power;
        Debug.Log($"{power}만큼의 체력을 회복했습니다. 남은 체력 : {Health}");
    }
}


public class Enemy : Creature
{
    public Enemy(int health, int stamina, int speed) : base(health, stamina, speed)
    {

    }
}