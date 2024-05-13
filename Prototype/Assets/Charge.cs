using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float maxCharge;
    public float charge;
    public float depleteSpeedPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        charge = maxCharge;
    }

    // Update is called once per frame
    void Update()
    {
        charge -= depleteSpeedPerSecond * Time.deltaTime;
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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Bonfire"))
        {
            PlayerBars player = FindAnyObjectByType<PlayerBars>();
            player.unit.charge = player.unit.maxCharge;
        }
    }
}
