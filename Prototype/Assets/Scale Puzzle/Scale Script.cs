using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public States condition;
    public States currentstate;
    public GameObject leftweight;
    public GameObject rightweight;
    public GameObject leftpos;
    public GameObject rightpos;

    public enum States
    {
        SAME,
        LEFTHEAVY,
        RIGHTHEAVY,
    }

    // Start is called before the first frame update
    void Start()
    {
        CompareWeight();
    }

    // Update is called once per frame
    void Update()
    {
        CompareWeight();
        leftweight.transform.position = leftpos.transform.position;
        rightweight.transform.position = rightpos.transform.position;
    }
    public void CompareWeight()
    {
        if (leftweight.GetComponent<WeightScript>().weight < rightweight.GetComponent<WeightScript>().weight)
        {
            leftpos.transform.localPosition = new Vector3(5.57f, -4.73f, 0);
            rightpos.transform.localPosition = new Vector3(-5.57f, -6.73f, 0);
            currentstate = States.RIGHTHEAVY;
        }
        else if(leftweight.GetComponent<WeightScript>().weight > rightweight.GetComponent<WeightScript>().weight)
        {
            leftpos.transform.localPosition = new Vector3(5.57f, -6.73f, 0);
            rightpos.transform.localPosition = new Vector3(-5.57f, -4.73f, 0);
            currentstate = States.LEFTHEAVY;
        }
        else
        {
            leftpos.transform.localPosition = new Vector3(5.57f, -5.73f, 0);
            rightpos.transform.localPosition = new Vector3(-5.57f, -5.73f, 0);
            currentstate= States.SAME;
        }
        transform.gameObject.GetComponent<WeightScript>().weight = leftweight.GetComponent<WeightScript>().weight + rightweight.GetComponent<WeightScript>().weight;
    }

}
