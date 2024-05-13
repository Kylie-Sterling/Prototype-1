using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public Image healthBar;
    public Image DelayedBar;

    public Charge unit;

    public Image stamBar;
    public Image stamDelayedBar;


    public float maxHealth;
    public float health;
    public float stamina;
    public float maxStamina;

    public float delayLerpSpeed = 0.05f;

    /*public Unit unit;*/
    //public PlayerHealth unit;
    // Start is called before the first frame update
    void Start()
    {
        //unit = GetComponent<EnemyHealth>();
        //unit = FindAnyObjectByType<PlayerHealth>();
        maxHealth = unit.maxCharge;
        health = maxHealth;
        //maxStamina = unit.maxStamina;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        health = unit.charge;
        float useHealth = health / maxHealth;
        if (useHealth != healthBar.fillAmount)
        {
            healthBar.fillAmount = useHealth;
        }
        if (healthBar != DelayedBar)
        {
            DelayedBar.fillAmount = Mathf.Lerp(DelayedBar.fillAmount, useHealth, delayLerpSpeed);
        }
       /* stamina = unit.stamina;
        float useStam = stamina / maxStamina;
        if (useStam != stamBar.fillAmount)
        {
            stamBar.fillAmount = useStam;
        }
        if (stamBar != stamDelayedBar)
        {
            if (stamBar.fillAmount > stamDelayedBar.fillAmount)
            {
                stamDelayedBar.fillAmount = stamBar.fillAmount;
            }
            else
            {
                stamDelayedBar.fillAmount = Mathf.Lerp(stamDelayedBar.fillAmount, useStam, delayLerpSpeed);
            }
        }*/
    }
    
}
