using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) //destroys the scrap and adds to the scrap total
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScrapCounter.totalscrap++;
            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(2);
            Destroy(transform.parent.gameObject);
        }
    }
}
