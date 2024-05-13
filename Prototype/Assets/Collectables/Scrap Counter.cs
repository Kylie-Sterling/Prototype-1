using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrapCounter : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    public static int totalscrap; //this static can be called from anywhere which means anything can increment it which is probably bad code but it works and that's what matters
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Counter.text = totalscrap.ToString(); //changes the text of the scrap counter to the total scrap
    }
}
