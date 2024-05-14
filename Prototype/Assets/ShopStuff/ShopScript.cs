using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopScript : MonoBehaviour
{
    int[,] pandq = new int[7, 2];
    int page = 0;
    GameObject[,] pages = new GameObject[3, 3];
    public GameObject stock1;
    public int price1;
    public int quantity1;
    public GameObject stock2;
    public int price2;
    public int quantity2;
    public GameObject stock3;
    public int price3;
    public int quantity3;
    public GameObject stock4;
    public int price4;
    public int quantity4;
    public GameObject stock5;
    public int price5;
    public int quantity5;
    public GameObject stock6;
    public int price6;
    public int quantity6;
    public GameObject stock7;
    public int price7;
    public int quantity7;
    GameObject item1;
    GameObject item2;
    GameObject item3;
    GameObject temp1;
    GameObject temp2;
    GameObject temp3;
    // Start is called before the first frame update
    void Start()
    {
        pandq[0, 0] = price1;
        pandq[0, 1] = quantity1;

        pandq[1, 0] = price2;
        pandq[1, 1] = quantity2;

        pandq[2, 0] = price3;
        pandq[2, 1] = quantity3;

        pandq[3, 0] = price4;
        pandq[3, 1] = quantity4;

        pandq[4, 0] = price5;
        pandq[4, 1] = quantity5;

        pandq[5, 0] = price6;
        pandq[5, 1] = quantity6;

        pandq[6, 0] = price7;
        pandq[6, 1] = quantity7;

        pages[0, 0] = stock1;
        pages[0, 1] = stock2;
        pages[0, 2] = stock3;

        pages[1, 0] = stock4;
        pages[1, 1] = stock5;
        pages[1, 2] = stock6;

        pages[2, 0] = stock7;

        item1 = pages[page, 0];
        item2 = pages[page, 1];
        item3 = pages[page, 2];
        temp1 = Instantiate(item1, transform.position + new Vector3(0, 0, 2), Quaternion.Euler(transform.forward));
        temp2 = Instantiate(item2, transform.position + new Vector3(0, 0, 4), Quaternion.Euler(transform.forward));
        temp3 = Instantiate(item3, transform.position + new Vector3(0, 0, 6), Quaternion.Euler(transform.forward));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            page++;
            if (page > 2)
            {
                page = 0;
            }
            
        }
        DisplayPage();
    }
    void DisplayPage()
    {
        Destroy(temp1);
        Destroy(temp2);
        Destroy(temp3); 
        item1 = pages[page, 0];
        item2 = pages[page, 1];
        item3 = pages[page, 2];
        if(item1 != null)
        {
            temp1 = Instantiate(item1, transform.position + new Vector3(0, 0, 2), Quaternion.Euler(transform.forward));
        }
        if (item2 != null)
        {
            temp2 = Instantiate(item2, transform.position + new Vector3(0, 0, 4), Quaternion.Euler(transform.forward));
        }
        if (item3 != null)
        {
            temp3 = Instantiate(item3, transform.position + new Vector3(0, 0, 6), Quaternion.Euler(transform.forward));
        }
    }
    public void buyitem(int i)
    {
        if (pandq[page*3+i,1] > 0 && ScrapCounter.totalscrap >= pandq[page * 3 + i, 0])
        {
            ScrapCounter.totalscrap -= pandq[page * 3 + i, 0];
            pandq[page * 3 + i, 1]--;
        }
        if (pandq[page*3+i,1] <= 0)
        {
            pages[page, i] = null;
            DisplayPage();
        }
    }
}
