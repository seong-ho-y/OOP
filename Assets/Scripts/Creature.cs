using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

//Creature Ŭ������ �������� �⺻���� �͵�
//TakeDamage, Heal, UseStamina, 
public class Creature
{
    private int _health;
    public int Health
    {
        get { return _health; }
        private set
        {
            if (value < 0) _health = 0; //��ȿ���˻縦 ���� ������ ������ �� �ִ�
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
        Debug.Log($"{damage}��ŭ�� ���ظ� �Ծ����ϴ�. ���� ü�� : {Health}");
    }
    public void Heal(int power)
    {
        Health += power;
        Debug.Log($"{power}��ŭ�� ü���� ȸ���߽��ϴ�. ���� ü�� : {Health}");
    }
}


public class Enemy : Creature
{
    public Enemy(int health, int stamina, int speed) : base(health, stamina, speed)
    {

    }
}