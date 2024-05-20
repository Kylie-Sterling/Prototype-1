using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other) //destroys the scrap and adds to the scrap total
    {
        ScrapCounter.totalscrap++;
        Audio a = FindAnyObjectByType<Audio>();
        a.PlaySound(2);
        Destroy(transform.parent.gameObject);
    }
}
