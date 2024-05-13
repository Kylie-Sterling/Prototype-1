using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float maxCharge;
    public float charge;
    public float depleteSpeedPerSecond;

    public float damageCD;
    public float damageValue;

    public float iframeTime;
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
        if (other.gameObject.CompareTag("EnemyTag") && damageCD <= 0)
        {
            Charge c = FindAnyObjectByType<Charge>();
                c.charge -= damageValue;
            damageCD = iframeTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Bonfire"))
        {
            PlayerBars player = FindAnyObjectByType<PlayerBars>();
            player.unit.charge = player.unit.maxCharge;
        }
        if (collision.gameObject.CompareTag("EnemyTag") && damageCD <= 0)
        {
            Debug.Log("Damage");
            Charge c = FindAnyObjectByType<Charge>();
                c.charge -= damageValue;
            damageCD = iframeTime;

        }
    }
}
