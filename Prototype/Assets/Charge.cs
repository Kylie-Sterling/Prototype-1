using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Charge : MonoBehaviour
{
    public float maxCharge;
    public float charge;
    public float depleteSpeedPerSecond;

    public float damageCD;
    public float damageValue;

    public float iframeTime;

    public bool invincible;
    // Start is called before the first frame update
    void Start()
    {
        charge = maxCharge;
        damageValue = (maxCharge / 5);
    }

    // Update is called once per frame
    void Update()
    {
        charge -= depleteSpeedPerSecond * Time.deltaTime;
        damageCD -= 1 * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Bonfire"))
        {
            PlayerBars player = FindAnyObjectByType<PlayerBars>();
            player.unit.charge = player.unit.maxCharge;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("EnemyTag") && (damageCD <= 0 && !invincible))
        {
            damageValue = collision.gameObject.GetComponent<EnemyDamage>().damageValue;

            Debug.Log("Damage: " + damageValue.ToString());

            charge -= damageValue;
            damageCD = iframeTime;
            if (charge <= 0)
            {
                OnDeath();
            }
        }
    }
    void OnDeath()
    {
        string sceneName = SceneManager.GetActiveScene().ToString();
        SceneManager.LoadScene(0);
    }
}