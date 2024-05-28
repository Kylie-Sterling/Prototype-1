using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBars player = FindAnyObjectByType<PlayerBars>();
            player.unit.charge = player.unit.maxCharge;

            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(3);
        }
    }
}
