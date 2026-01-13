using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int damage;
    public int maxHp;

    [SerializeField]private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHp;
    }

    public virtual void TakeDamage()
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
