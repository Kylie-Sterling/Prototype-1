using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightTrigger : MonoBehaviour
{
    public GameObject weightobject;
    public Boolean active;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && active == true)
        {
            if (Input.GetButtonDown("Jump") && ScrapCounter.totalscrap > 0)
            {
                ScrapCounter.totalscrap--;
                weightobject.GetComponent<WeightScript>().weight++;
            }
        }
    }
}
