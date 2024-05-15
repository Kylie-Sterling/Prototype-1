using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBar;
    public Image DelayedBar;

    public EnemyHealth unit;

    public float maxHealth;
    public float health;

    public float delayLerpSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = unit.maxHealth;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = unit.health;
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
