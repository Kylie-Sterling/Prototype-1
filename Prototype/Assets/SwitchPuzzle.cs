using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public SwitchPuzzleSwitch[] switches;
    public Material onMat;
    public Material offMat;

    public GameObject winObject;
    public enum wins
    {
        DOOR,
        SCRAP,
    }
    public wins win;
    private void Start()
    {
        for(int i = 0; i < switches.Length; i++)
        {
            switches[i].ID = i;
            switches[i].puzzle = this;
            switches[i].Toggle();
        }
    }
    public void CheckWin()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (!switches[i].on)
            {
                Debug.Log(switches[i].ID.ToString());

                return;
            }
        }
        Debug.Log("WINNER");
        Win();
    }
    void Win()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].gameObject.SetActive(false);
        }
        switch (win)
        {
            case wins.DOOR:
                winObject.SetActive(false);
                break;
            case wins.SCRAP:
                GameObject o = Instantiate(winObject, transform);
                o.transform.position = transform.position;
                break;
            default:

                break;
        }
    }
}
