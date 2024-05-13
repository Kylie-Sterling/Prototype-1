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

    public float maxHealth;
    public float health;

    public float delayLerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = unit.maxCharge;
        health = maxHealth;
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
    }
    
}
