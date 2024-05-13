using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float health;

    public float maxHealth;
    public float chargeRestore;
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Charge c = FindAnyObjectByType<Charge>();
            c.charge += chargeRestore;
            if(c.charge > c.maxCharge)
            {
                c.charge = c.maxCharge;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        WeaponSelecter w = FindAnyObjectByType<WeaponSelecter>();
        if(weapon != null && w.hitboxOn)
        {
            TakeDamage(weapon.attackDamage);
        }
    }
}
