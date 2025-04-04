using System;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHP;
    public IntVariable hp;
    public int MaxHp {get=>hp.maxValue;}
    public int CurrentHp {get=>hp.currentValue; set=>hp.SetValue(value);}

    protected Animator animator;
    
    public bool isDead = false;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hp.maxValue = maxHP;
        CurrentHp = MaxHp;
    }
    
    public virtual void TakeDamage(int damage)
    {
        if(CurrentHp > damage)
        {
            CurrentHp -= damage;
            Debug.Log("Current HP: " + CurrentHp);
        }
        else
        {
            CurrentHp = 0;
            //Die();
            isDead = true;
            
        }
    }
}
