using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiegeticHealth : MonoBehaviour
{
    public GameObject[] healthBlocks;
    public Charge charge;
    public Material on;
    public Material off;

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(charge.charge != health)
        {
            health = charge.charge;
            for(int i = 0; i < healthBlocks.Length; i++)
            {
                MeshRenderer mesh = healthBlocks[i].GetComponent<MeshRenderer>();

                if (i+1 <= health)
                {
                    //MeshRenderer mesh = healthBlocks[i].GetComponent<MeshRenderer>();
                    mesh.material = on;
                }
                else
                {
                    mesh.material = off;
                }
            }
        }
    }
}
