using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemnum;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.parent.gameObject.GetComponent<ShopScript>().buyitem(itemnum);
        }
    }
}
