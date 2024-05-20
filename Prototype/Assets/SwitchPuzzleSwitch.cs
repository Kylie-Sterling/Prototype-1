using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzleSwitch : MonoBehaviour
{
    public int ID;
    public bool on;
    
    public SwitchPuzzle puzzle;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Audio a = FindAnyObjectByType<Audio>();
            a.PlaySound(0);
            Toggle();
            int check = ID - 1;
            Debug.Log(check);
            if (check >= 0)
            {
                Debug.Log("lower switch");

                puzzle.switches[check].Toggle();
            }
            check = ID + 1;
            Debug.Log(check);
            if (check < puzzle.switches.Length)
            {
                Debug.Log("upper switch");
                puzzle.switches[check].Toggle();
            }

            puzzle.CheckWin();
        }
    }
    public void Toggle()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        on = !on;
        if(on)
        {
            mr.material = puzzle.onMat;
        }
        else
        {
            mr.material = puzzle.offMat;
        }
    }
}
