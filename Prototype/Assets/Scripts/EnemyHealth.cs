using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float health;
    public float timer;
    public float maxHealth;
    public float chargeRestore;
    public Weapon.element currentEffect = Weapon.element.none;
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        switch (currentEffect)
        {
            case Weapon.element.Fire:
                TakeDamage(0.01f);
                break;
            case Weapon.element.Water:
                break;
            case Weapon.element.Ground:
                GetComponent<NavMeshAgent>().speed = 1.5f;
                break;
            case Weapon.element.Electric:
                break;
            default:
                break;
        }
        if (timer < 0)
        {
            GetComponent<NavMeshAgent>().speed = 3f;
            currentEffect = Weapon.element.none;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        Audio a = FindAnyObjectByType<Audio>();
        a.PlaySound(1);
        if (health <= 0)
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
        if(weapon.FusedElement != Weapon.element.none && currentEffect == Weapon.element.none)
        {
            timer = 5;
            currentEffect = weapon.FusedElement;
            Debug.Log(currentEffect);
        }
    }
}
