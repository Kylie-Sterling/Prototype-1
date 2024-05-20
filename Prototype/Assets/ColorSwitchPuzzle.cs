using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitchPuzzle : MonoBehaviour
{
    public ColorSwitchPuzzleSwitch[] switches;
    public Color[] solution;
    public Color[] colors;

    public GameObject winObject;
    public enum wins
    {
        DOOR,
        SCRAP,
    }
    public wins win;
    private void Start()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].ID = i;
            switches[i].puzzle = this;
        }
    }
    public void CheckWin()
    {
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].m.material.color != solution[i])
            {
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
